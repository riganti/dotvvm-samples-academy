using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

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
    }
}