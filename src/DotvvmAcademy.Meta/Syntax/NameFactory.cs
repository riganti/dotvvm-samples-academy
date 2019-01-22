using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace DotvvmAcademy.Meta.Syntax
{
    public static class NameFactory
    {
        public static ArrayTypeNameNode ArrayType(NameNode elementType, int rank)
        {
            Debug.Assert(elementType != null);
            Debug.Assert(rank > 0);
            return new ArrayTypeNameNode(
                elementType: elementType,
                openBracketToken: MissingToken(NameTokenKind.OpenBracket),
                closeBracketToken: MissingToken(NameTokenKind.CloseBracket),
                commaTokens: MissingTokens(NameTokenKind.Comma, rank - 1));
        }

        public static ConstructedTypeNameNode ConstructedType(NameNode unboundTypeName, IEnumerable<NameNode> typeArguments)
        {
            var arguments = typeArguments.ToImmutableArray();
            Debug.Assert(arguments.Length > 0);
            return new ConstructedTypeNameNode(
                unboundTypeName: unboundTypeName,
                arguments: arguments,
                commaTokens: MissingTokens(NameTokenKind.Comma, arguments.Length - 1),
                openBracketToken: MissingToken(NameTokenKind.OpenBracket),
                closeBracketToken: MissingToken(NameTokenKind.CloseBracket));
        }

        public static GenericNameNode Generic(string identifier, int arity)
        {
            Debug.Assert(arity > 0);
            return new GenericNameNode(
                identifierToken: IdentifierToken(identifier),
                backtickToken: MissingToken(NameTokenKind.Backtick),
                arityToken: NumericLiteralToken(arity));
        }

        public static IdentifierNameNode Identifier(string identifier)
        {
            return new IdentifierNameNode(IdentifierToken(identifier));
        }

        public static NameToken IdentifierToken(string identifier)
        {
            Debug.Assert(!string.IsNullOrEmpty(identifier));
            return new NameToken(NameTokenKind.Identifier, identifier, 0, identifier.Length);
        }

        public static MemberNameNode Member(NameNode type, string identifier)
        {
            return new MemberNameNode(type, Identifier(identifier), MissingToken(NameTokenKind.ColonColon));
        }

        public static NameToken MissingToken(NameTokenKind kind)
        {
            return new NameToken(kind, null, -1, -1, true);
        }

        public static ImmutableArray<NameToken> MissingTokens(NameTokenKind kind, int count)
        {
            Debug.Assert(count >= 0);
            var commaBuilder = ImmutableArray.CreateBuilder<NameToken>();
            for (var i = 0; i < count; i++)
            {
                commaBuilder.Add(MissingToken(kind));
            }
            return commaBuilder.ToImmutable();
        }

        public static NestedTypeNameNode NestedType(NameNode type, SimpleNameNode name)
        {
            return new NestedTypeNameNode(type, name, MissingToken(NameTokenKind.Plus));
        }

        public static NestedTypeNameNode NestedType(NameNode type, string identifier, int arity = 0)
        {
            return NestedType(type, Simple(identifier, arity));
        }

        public static NameToken NumericLiteralToken(int number)
        {
            var @string = number.ToString();
            return new NameToken(NameTokenKind.NumericLiteral, @string, 0, @string.Length);
        }

        public static PointerTypeNameNode PointerType(NameNode elementType)
        {
            return new PointerTypeNameNode(elementType, MissingToken(NameTokenKind.Asterisk));
        }

        public static QualifiedNameNode Qualified(NameNode left, string identifier, int arity = 0)
        {
            return new QualifiedNameNode(left, Simple(identifier, arity), MissingToken(NameTokenKind.Dot));
        }

        public static SimpleNameNode Simple(string identifier, int arity = 0)
        {
            Debug.Assert(arity >= 0);
            return arity == 0
                ? Identifier(identifier)
                : (SimpleNameNode)Generic(identifier, arity);
        }
    }
}