using DotvvmAcademy.Meta.Syntax;
using System.Collections.Immutable;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public static class MemberInfoLocatorExtensions
    {
        public static ImmutableArray<MemberInfo> Locate(this IMemberInfoLocator locator, string name)
        {
            var lexer = new NameLexer(name);
            var parser = new NameParser(lexer);
            return locator.Locate(parser.Parse());
        }
    }
}