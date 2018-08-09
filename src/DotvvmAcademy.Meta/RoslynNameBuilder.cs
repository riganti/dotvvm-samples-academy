using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public class RoslynNameBuilder : INameBuilder<ISymbol>
    {
        public NameNode Build(ISymbol symbol)
        {
            return Visit(symbol);
        }

        private ImmutableArray<NameToken> GetCommaTokens(int count)
        {
            var commaBuilder = ImmutableArray.CreateBuilder<NameToken>();
            for (int i = 0; i < count; i++)
            {
                commaBuilder.Add(new NameToken(NameTokenKind.Comma, null, -1, -1, true));
            }
            return commaBuilder.ToImmutable();
        }

        private NameToken GetIdentifierToken(string identifier)
        {
            return new NameToken(NameTokenKind.Identifier, identifier, 0, identifier.Length - 1);
        }

        private NameToken GetMissingToken(NameTokenKind kind)
        {
            return new NameToken(kind, null, -1, -1, true);
        }

        private SimpleNameNode GetSimpleName(ISymbol symbol)
        {
            var identifierToken = GetIdentifierToken(symbol.Name);
            if (symbol is INamedTypeSymbol typeSymbol)
            {
                if (typeSymbol.Arity == 0)
                {
                    return new IdentifierNameNode(identifierToken);
                }

                var arityNumber = typeSymbol.Arity.ToString();
                var arityToken = new NameToken(NameTokenKind.NumericLiteral, arityNumber, 0, arityNumber.Length - 1);
                return new GenericNameNode(
                    identifierToken: identifierToken,
                    backtickToken: GetMissingToken(NameTokenKind.Backtick),
                    arityToken: arityToken);
            }

            return new IdentifierNameNode(identifierToken);
        }

        private NameNode Visit(ISymbol symbol)
        {
            switch (symbol)
            {
                case IArrayTypeSymbol arrayTypeSymbol:
                    return VisitArrayType(arrayTypeSymbol);

                case IPointerTypeSymbol pointerTypeSymbol:
                    return VisitPointerType(pointerTypeSymbol);

                case IMethodSymbol methodSymbol:
                case IPropertySymbol propertySymbol:
                case IEventSymbol eventSymbol:
                    return VisitMember(symbol);

                case INamedTypeSymbol constructedTypeSymbol when constructedTypeSymbol.ConstructedFrom != null:
                    return VisitConstructedType(constructedTypeSymbol);

                case INamedTypeSymbol nestedTypeSymbol when nestedTypeSymbol.ContainingType != null:
                    return VisitNestedType(nestedTypeSymbol);

                default:
                    return VisitQualifiedName(symbol);
            }
        }

        private ArrayTypeNameNode VisitArrayType(IArrayTypeSymbol arrayType)
        {
            return new ArrayTypeNameNode(
                elementType: Visit(arrayType.ElementType),
                openBracketToken: GetMissingToken(NameTokenKind.OpenBracket),
                closeBracketToken: GetMissingToken(NameTokenKind.CloseBracket),
                commaTokens: GetCommaTokens(arrayType.Rank));
        }

        private ConstructedTypeNameNode VisitConstructedType(INamedTypeSymbol constructedTypeSymbol)
        {
            var typeArguments = constructedTypeSymbol.TypeArguments.Select(Visit).ToImmutableArray();
            var typeArgumentList = new TypeArgumentListNode(
                arguments: typeArguments,
                commaTokens: GetCommaTokens(typeArguments.Length),
                openBracketToken: GetMissingToken(NameTokenKind.OpenBracket),
                closeBracketToken: GetMissingToken(NameTokenKind.CloseBracket));
            return new ConstructedTypeNameNode(
                unboundTypeName: Visit(constructedTypeSymbol.ConstructedFrom),
                typeArgumentList: typeArgumentList);
        }

        private MemberNameNode VisitMember(ISymbol symbol)
        {
            var identifier = new IdentifierNameNode(GetIdentifierToken(symbol.Name));
            return new MemberNameNode(
                type: Visit(symbol.ContainingType),
                member: identifier,
                colonColonToken: GetMissingToken(NameTokenKind.ColonColon));
        }

        private NestedTypeNameNode VisitNestedType(INamedTypeSymbol nestedType)
        {
            return new NestedTypeNameNode(
                left: Visit(nestedType.ContainingType),
                right: GetSimpleName(nestedType),
                plusToken: GetMissingToken(NameTokenKind.Plus));
        }

        private PointerTypeNameNode VisitPointerType(IPointerTypeSymbol pointerTypeSymbol)
        {
            return new PointerTypeNameNode(
                elementType: Visit(pointerTypeSymbol.PointedAtType),
                asteriskToken: GetMissingToken(NameTokenKind.Asterisk));
        }

        private NameNode VisitQualifiedName(ISymbol symbol)
        {
            var simpleName = GetSimpleName(symbol);
            if (symbol.ContainingNamespace.IsGlobalNamespace)
            {
                return simpleName;
            }

            return new QualifiedNameNode(
                left: VisitQualifiedName(symbol.ContainingNamespace),
                right: simpleName,
                dotToken: GetMissingToken(NameTokenKind.Dot));
        }
    }
}