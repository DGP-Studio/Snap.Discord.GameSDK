using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

namespace Snap.Discord.GameSDK.SourceGeneration;

[Generator(LanguageNames.CSharp)]
internal sealed class MethodPointerAsStructGenerator : IIncrementalGenerator
{
    private const string FileName = "ProtoTypes.txt";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<ImmutableArray<AdditionalText>> provider = context.AdditionalTextsProvider.Where(MatchFileName).Collect();

        context.RegisterImplementationSourceOutput(provider, GenerateIdentityStructs);
    }

    private static bool MatchFileName(AdditionalText text)
    {
        return Path.GetFileName(text.Path) == FileName;
    }

    private static void GenerateIdentityStructs(SourceProductionContext context, ImmutableArray<AdditionalText> texts)
    {
        AdditionalText jsonFile = texts.Single();

        string protoTypeText = jsonFile.GetText(context.CancellationToken)!.ToString();
        List<MethodPointerInfo> infoList = ParseMethodPointerInfoList(protoTypeText);

        GenerateMethodPointerStruct(context, infoList);
    }

    private static List<MethodPointerInfo> ParseMethodPointerInfoList(string text)
    {
        List<MethodPointerInfo> results = new();
        using (StringReader reader = new(text))
        {
            while(reader.ReadLine() is string line)
            {
                if (string.IsNullOrEmpty(line) || line.StartsWith("//"))
                {
                    continue;
                }

                MethodPointerInfo info = new();

                ReadOnlySpan<char> lineSpan = line.AsSpan();
                int firstSpaceIndex = lineSpan.IndexOf(' ');
                info.ReturnType = ReplaceWithFullQualifiedType(lineSpan.Slice(0, lineSpan.IndexOf(' ')).ToString());

                lineSpan = lineSpan.Slice(firstSpaceIndex + 1);
                int leftParenthesisIndex = lineSpan.IndexOf('(');
                info.MethodName = lineSpan.Slice(0, leftParenthesisIndex).ToString();

                lineSpan = lineSpan.Slice(leftParenthesisIndex + 1);
                int rightParenthesisIndex = lineSpan.LastIndexOf(')');
                string parameters = lineSpan.Slice(0, rightParenthesisIndex).ToString();

                if (parameters.Length > 0)
                {
                    info.Parameters = new();
                    foreach (string parameter in parameters.Split(','))
                    {
                        ReadOnlySpan<char> parameterSpan = parameter.Trim().AsSpan();
                        int spaceIndex = parameterSpan.LastIndexOf(' ');
                        string type = parameterSpan.Slice(0, spaceIndex).ToString();
                        string name = parameterSpan.Slice(spaceIndex + 1).ToString();
                        type = ReplaceWithFullQualifiedType(type);

                        info.Parameters.Add((type, name));
                    }
                }

                results.Add(info);
            }
        }

        return results;
    }

    private static string ReplaceWithFullQualifiedType(string type)
    {
        // Intercept string type first.
        type = type switch
        {
            "[MarshalAs(UnmanagedType.LPStr)] string" => "byte*",
            "[MarshalAs(UnmanagedType.LPStr)] ref string" => "byte*",
            "ref string" => "byte*",
            "string" => "byte*",
            "StringBuilder" => "byte*",
            "byte[]" => "byte*",
            _ => ReplaceWithFullQualifiedTypeInner(type),
        };

        return type;

        string ReplaceWithFullQualifiedTypeInner(string type)
        {
            bool hasRefPrefix = false;
            if (type.StartsWith("ref "))
            {
                type = type.Substring(4);
                hasRefPrefix = true;
            }

            type = type switch
            {
                "IntPtr" => "nint",
                "Int32" => "int",
                "UInt32" => "uint",
                "Int64" => "long",
                "UInt64" => "ulong",
                _ => type,
            };

            if (hasRefPrefix)
            {
                type = $"ref {type}";
            }

            return type;
        }
    }

    private static void GenerateMethodPointerStruct(SourceProductionContext context, List<MethodPointerInfo> groupedInfo)
    {
        StringBuilder sourceBuilder = new StringBuilder().AppendLine($$"""
            using Snap.Discord.GameSDK;
            using System;

            namespace Snap.Discord.GameSDK.ABI;
            """);

        foreach(MethodPointerInfo info in groupedInfo)
        {
            string delegateParameters = MakeDelegateParameters(info);

            string accessModifier = info.MethodName.EndsWith("Handler") ? "public" : "internal";

            sourceBuilder.AppendLine($$"""

            /// <summary>
            /// A wrapper for native function pointer
            /// <para/>
            /// {{info.ReturnType}} (*thisPtr)({{delegateParameters.Substring(0, delegateParameters.LastIndexOf(','))}})
            /// </summary>
            {{accessModifier}} unsafe struct {{info.MethodName}}
            {
                private delegate* unmanaged[Stdcall]<{{delegateParameters}}> thisPtr;

                internal {{info.ReturnType}} Invoke({{MakeInvokeMethodParameters(info)}})
                {
                    {{(info.ReturnType is "void" ? "" : "return ")}}thisPtr({{MakePointerParameters(info)}});
                }

                public static {{info.MethodName}} Create(delegate* unmanaged[Stdcall]<{{delegateParameters}}> ptr)
                {
                    {{info.MethodName}} value = default;
                    value.thisPtr = ptr;
                    return value;
                }
            }
            """);
        }

        context.AddSource($"Snap.Discord.GameSDK.ABI.g.cs", sourceBuilder.ToString());
    }

    private static string MakeDelegateParameters(MethodPointerInfo info)
    {
        StringBuilder delegateParameterBuilder = new();
        int current = 0;
        while(current < info.Parameters.Count)
        {
            (string type, _) = info.Parameters[current];
            if (type.StartsWith("ref"))
            {
                // remake as a pointer type
                delegateParameterBuilder.Append(type.Substring(4)).Append('*');
            }
            else
            {
                delegateParameterBuilder.Append(type);
            }
            
            current++;
            delegateParameterBuilder.Append(", ");
        }
        delegateParameterBuilder.Append(info.ReturnType);
        return delegateParameterBuilder.ToString();
    }

    private static string MakeInvokeMethodParameters(MethodPointerInfo info)
    {
        StringBuilder parameterBuilder = new();
        int current = 0;
        while (current < info.Parameters.Count)
        {
            (string type, string name) = info.Parameters[current];

            if (type.StartsWith("ref"))
            {
                // remake as a pointer type
                parameterBuilder.Append(type.Substring(4)).Append('*');
            }
            else
            {
                parameterBuilder.Append(type);
            }

            parameterBuilder.Append(' ').Append(name);
            current++;
            if (current < info.Parameters.Count)
            {
                parameterBuilder.Append(", ");
            }
        }

        return parameterBuilder.ToString();
    }

    private static string MakePointerParameters(MethodPointerInfo info)
    {
        StringBuilder parameterBuilder = new();
        int current = 0;
        while (current < info.Parameters.Count)
        {
            (_, string name) = info.Parameters[current];
            parameterBuilder.Append(name);
            current++;
            if (current < info.Parameters.Count)
            {
                parameterBuilder.Append(", ");
            }
        }

        return parameterBuilder.ToString();
    }

    private sealed class MethodPointerInfo
    {
        public string ReturnType { get; set; } = default!;

        public string MethodName { get; set; } = default!;

        public List<(string Type, string Name)> Parameters { get; set; } = default!;
    }
}
