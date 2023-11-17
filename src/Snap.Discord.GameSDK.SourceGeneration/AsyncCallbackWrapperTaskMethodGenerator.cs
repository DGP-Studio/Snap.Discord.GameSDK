using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snap.Discord.GameSDK.SourceGeneration;

[Generator(LanguageNames.CSharp)]
internal sealed class AsyncCallbackWrapperTaskMethodGenerator : IIncrementalGenerator
{
    private const string AttributeName = "Snap.Discord.GameSDK.AsyncCallbackAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<ImmutableArray<GeneratorSyntaxContext2<IMethodSymbol>>> callbacks = context.SyntaxProvider
            .CreateSyntaxProvider(FilterAttributedMethods, AsyncCallbackMethod)
            .Where(GeneratorSyntaxContext2<IMethodSymbol>.NotNull)
            .Collect();

        context.RegisterPostInitializationOutput(GenerateAllAttributes);
        context.RegisterSourceOutput(callbacks, GenerateCallbackAsyncTaskImplementations);
    }

    public static void GenerateAllAttributes(IncrementalGeneratorPostInitializationContext context)
    {
        string annotations = """
            using System;

            namespace Snap.Discord.GameSDK;

            [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
            internal sealed class AsyncCallbackAttribute : Attribute
            {
            }
            """;
        context.AddSource($"{AttributeName}.g.cs", annotations);
    }

    private static bool FilterAttributedMethods(SyntaxNode node, CancellationToken token)
    {
        return node is MethodDeclarationSyntax methodDeclarationSyntax
            && methodDeclarationSyntax.Parent is ClassDeclarationSyntax classDeclarationSyntax
            && classDeclarationSyntax.Modifiers.Count > 1
            && methodDeclarationSyntax.HasAttributeLists();
    }

    private static GeneratorSyntaxContext2<IMethodSymbol> AsyncCallbackMethod(GeneratorSyntaxContext context, CancellationToken token)
    {
        if (context.TryGetDeclaredSymbol(token, out IMethodSymbol? methodSymbol))
        {
            ImmutableArray<AttributeData> attributes = methodSymbol!.GetAttributes();
            if (attributes.Any(data => data.AttributeClass!.ToDisplayString() == AttributeName))
            {
                return new(context, methodSymbol, attributes);
            }
        }

        return default;
    }

    private static void GenerateCallbackAsyncTaskImplementations(SourceProductionContext production, ImmutableArray<GeneratorSyntaxContext2<IMethodSymbol>> context2s)
    {
        foreach (GeneratorSyntaxContext2<IMethodSymbol> context2 in context2s/*.DistinctBy(c => c.Symbol.ToDisplayString())*/)
        {
            GenerateCallbackAsyncTaskImplementation(production, context2);
        }
    }

    private static void GenerateCallbackAsyncTaskImplementation(SourceProductionContext production, GeneratorSyntaxContext2<IMethodSymbol> context2)
    {
        INamedTypeSymbol classSymbol = context2.Symbol.ContainingType;
        string className = classSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);

        string methodName = context2.Symbol.Name;

        string parameterTypes = string.Join(", ", context2.Symbol.Parameters.Select(p => p.Type.ToDisplayString()));
        string parameterWithNamesSkipLastOne = string.Join(", ", context2.Symbol.Parameters.SkipLast(1).Select(p => $"{p.Type.ToDisplayString()} {p.Name}"));

        StringBuilder parameterSkipLastOneBuilder = new();
        foreach(IParameterSymbol parameterSymbol in context2.Symbol.Parameters.SkipLast(1))
        {
            parameterSkipLastOneBuilder.Append($"{parameterSymbol.Name}, ");
        }

        string parameterSkipLastOne = parameterSkipLastOneBuilder.ToString();

        string handlerName = context2.Symbol.Parameters.Last().Type.ToDisplayString();

        string code = $$"""
            using System.Runtime.CompilerServices;
            using System.Runtime.InteropServices;
            using System.Threading;
            using System.Threading.Tasks;
            using Snap.Discord.GameSDK.ABI;

            namespace {{classSymbol.ContainingNamespace}};

            #nullable enable

            partial class {{className}}
            {
                /// <inheritdoc cref="{{methodName}}({{parameterTypes}})"/>
                public async Task<Result> {{methodName}}Async({{parameterWithNamesSkipLastOne}})
                {
                    unsafe void UnsafeInvoke()
                    {
                        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
                        static unsafe void HandlerImpl(Result result)
                        {
                            if (callbackResults.TryAdd(nameof({{methodName}}), result))
                            {
                                if (callbackWaits.TryGetValue(nameof({{methodName}}), out ManualResetEventSlim? slim))
                                {
                                    slim.Set();
                                    return;
                                }
                            }

                            ThrowHelper.InvalidOperation();
                        }

                        {{methodName}}({{parameterSkipLastOne}}{{handlerName}}.Create(&HandlerImpl));
                    }

                    ThrowHelper.InvalidOperationIf(callbackWaits.ContainsKey(nameof({{methodName}})));
                    if (callbackWaits.TryAdd(nameof({{methodName}}), new()))
                    {
                        UnsafeInvoke();
                        if (callbackWaits.TryGetValue(nameof({{methodName}}), out ManualResetEventSlim? slim))
                        {
                            await Task.CompletedTask.ConfigureAwait(ConfigureAwaitOptions.ForceYielding);
                            slim.Wait();
                            if (callbackWaits.TryRemove(nameof({{methodName}}), out _))
                            {
                                if (callbackResults.TryRemove(nameof({{methodName}}), out Result result))
                                {
                                    return result;
                                }
                            }
                        }
                    }

                    ThrowHelper.InvalidOperation();
                    return default;
                }
            }
            """;

        StringBuilder stringBuilder = new(classSymbol.ToDisplayString());
        stringBuilder.Replace('<', '{').Replace('>', '}');
        string normalizedClassName = stringBuilder.ToString();
        production.AddSource($"{normalizedClassName}.{methodName}.g.cs", code);
    }
}