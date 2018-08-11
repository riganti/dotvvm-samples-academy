using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public static class NameFactory
    {
        public static NameToken IdentifierToken(string identifier)
        {
            return new NameToken(NameTokenKind.Identifier, identifier, 0, identifier.Length - 1);
        }

        public static NameToken MissingToken(NameTokenKind kind)
        {
            return new NameToken(kind, null, -1, -1, true);
        }

        public static ImmutableArray<NameToken> MissingTokens(NameTokenKind kind, int count)
        {
            var commaBuilder = ImmutableArray.CreateBuilder<NameToken>();
            for (var i = 0; i < count; i++)
            {
                commaBuilder.Add(MissingToken(kind));
            }
            return commaBuilder.ToImmutable();
        }

        public static NameToken NumericLiteralToken(int number)
        {
            var @string = number.ToString();
            return new NameToken(NameTokenKind.NumericLiteral, @string, 0, @string.Length - 1);
        }
    }
}