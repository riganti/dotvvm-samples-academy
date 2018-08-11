using DotvvmAcademy.Meta.Syntax;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta
{
    public static class LocatorExtensions
    {
        public static ImmutableArray<TMeta> Locate<TMeta>(this IMetaLocator<TMeta> locator, string name)
        {
            var lexer = new NameLexer(name);
            var parser = new NameParser(lexer);
            return locator.Locate(parser.Parse());
        }

        public static TMeta LocateSingle<TMeta>(this IMetaLocator<TMeta> locator, NameNode name)
        {
            var result = locator.Locate(name);
            if (result.Length != 1)
            {
                throw new ArgumentException($"Could not locate single '{name}'. Found '{result.Length}'.");
            }

            return result[0];
        }

        public static TMeta LocateSingle<TMeta>(this IMetaLocator<TMeta> locator, string name)
        {
            var lexer = new NameLexer(name);
            var parser = new NameParser(lexer);
            return locator.LocateSingle(parser.Parse());
        }
    }
}