using Microsoft.CodeAnalysis;
using System.Threading;

namespace Snap.Discord.GameSDK.SourceGeneration;

internal static class GeneratorSyntaxContextExtension
{
    public static bool TryGetDeclaredSymbol<TSymbol>(this GeneratorSyntaxContext context, CancellationToken token, out TSymbol? symbol)
        where TSymbol : class, ISymbol
    {
        symbol = context.SemanticModel.GetDeclaredSymbol(context.Node, token) as TSymbol;
        return symbol != null;
    }
}