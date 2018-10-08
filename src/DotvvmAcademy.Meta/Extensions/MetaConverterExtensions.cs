using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public static class MetaConverterExtensions
    {
        public static IEnumerable<MemberInfo> Convert(this IMetaConverter<NameNode, MemberInfo> converter, string name)
        {
            var lexer = new NameLexer(name);
            var parser = new NameParser(lexer);
            return converter.Convert(parser.Parse());
        }

        public static IEnumerable<ISymbol> Convert(this IMetaConverter<NameNode, ISymbol> converter, string name)
        {
            var lexer = new NameLexer(name);
            var parser = new NameParser(lexer);
            return converter.Convert(parser.Parse());
        }
    }
}