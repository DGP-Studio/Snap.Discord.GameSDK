using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;

namespace Snap.Discord.GameSDK.SourceGeneration;

internal readonly struct GeneratorSyntaxContext2<TSymbol>
    where TSymbol : ISymbol
{
    public readonly GeneratorSyntaxContext Context;
    public readonly TSymbol Symbol;
    public readonly ImmutableArray<AttributeData> Attributes;
    public readonly bool HasValue = false;

    public GeneratorSyntaxContext2(GeneratorSyntaxContext context, TSymbol symbol, ImmutableArray<AttributeData> attributes)
    {
        Context = context;
        Symbol = symbol;
        Attributes = attributes;
        HasValue = true;
    }

    public static bool NotNull(GeneratorSyntaxContext2<TSymbol> context)
    {
        return context.HasValue;
    }

    public AttributeData SingleAttribute(string name)
    {
        return Attributes.Single(attribute => attribute.AttributeClass!.ToDisplayString() == name);
    }
}