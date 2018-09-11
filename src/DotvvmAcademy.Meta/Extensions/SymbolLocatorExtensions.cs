using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public static class SymbolLocatorExtensions
    {
        public static ImmutableArray<ISymbol> Locate(this ISymbolLocator locator, string name)
        {
            var lexer = new NameLexer(name);
            var parser = new NameParser(lexer);
            return locator.Locate(parser.Parse());
        }

        public static ImmutableArray<TSymbol> Locate<TSymbol>(this ISymbolLocator locator, NameNode name)
            where TSymbol : ISymbol
        {
            return locator.Locate(name).Cast<TSymbol>().ToImmutableArray();
        }

        public static ImmutableArray<TSymbol> Locate<TSymbol>(this ISymbolLocator locator, string name)
            where TSymbol : ISymbol
        {
            return locator.Locate(name).Cast<TSymbol>().ToImmutableArray();
        }
    }
}