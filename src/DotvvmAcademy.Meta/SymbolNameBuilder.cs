using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public class SymbolNameBuilder : ISymbolNameBuilder
    {
        public NameNode Build(ISymbol symbol)
        {
            return Visit(symbol);
        }

        private IdentifierNameNode GetIdentifier(ISymbol symbol)
        {
            return new IdentifierNameNode(NameFactory.IdentifierToken(symbol.Name));
        }

        private SimpleNameNode GetSimpleName(ISymbol symbol)
        {
            var identifierToken = NameFactory.IdentifierToken(symbol.Name);
            if (symbol is INamedTypeSymbol typeSymbol && typeSymbol.Arity > 0)
            {
                return new GenericNameNode(
                    identifierToken: identifierToken,
                    backtickToken: NameFactory.MissingToken(NameTokenKind.Backtick),
                    arityToken: NameFactory.NumericLiteralToken(typeSymbol.Arity));
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

                case INamedTypeSymbol constructedTypeSymbol when constructedTypeSymbol.ConstructedFrom != null:
                    return VisitConstructedType(constructedTypeSymbol);

                case INamedTypeSymbol nestedTypeSymbol when nestedTypeSymbol.ContainingType != null:
                    return VisitNestedType(nestedTypeSymbol);

                case IMethodSymbol methodSymbol:
                case IPropertySymbol propertySymbol:
                case IEventSymbol eventSymbol:
                    return VisitMember(symbol);

                default:
                    return VisitQualifiedName(symbol);
            }
        }

        private ArrayTypeNameNode VisitArrayType(IArrayTypeSymbol arrayType)
        {
            return new ArrayTypeNameNode(
                elementType: Visit(arrayType.ElementType),
                openBracketToken: NameFactory.MissingToken(NameTokenKind.OpenBracket),
                closeBracketToken: NameFactory.MissingToken(NameTokenKind.CloseBracket),
                commaTokens: NameFactory.MissingTokens(NameTokenKind.Comma, arrayType.Rank));
        }

        private ConstructedTypeNameNode VisitConstructedType(INamedTypeSymbol constructedTypeSymbol)
        {
            var typeArguments = constructedTypeSymbol.TypeArguments.Select(Visit).ToImmutableArray();
            var typeArgumentList = new TypeArgumentListNode(
                arguments: typeArguments,
                commaTokens: NameFactory.MissingTokens(NameTokenKind.Comma, typeArguments.Length),
                openBracketToken: NameFactory.MissingToken(NameTokenKind.OpenBracket),
                closeBracketToken: NameFactory.MissingToken(NameTokenKind.CloseBracket));
            return new ConstructedTypeNameNode(
                unboundTypeName: Visit(constructedTypeSymbol.ConstructedFrom),
                typeArgumentList: typeArgumentList);
        }

        private MemberNameNode VisitMember(ISymbol symbol)
        {
            return new MemberNameNode(
                type: Visit(symbol.ContainingType),
                member: GetIdentifier(symbol),
                colonColonToken: NameFactory.MissingToken(NameTokenKind.ColonColon));
        }

        private NestedTypeNameNode VisitNestedType(INamedTypeSymbol nestedType)
        {
            return new NestedTypeNameNode(
                left: Visit(nestedType.ContainingType),
                right: GetSimpleName(nestedType),
                plusToken: NameFactory.MissingToken(NameTokenKind.Plus));
        }

        private PointerTypeNameNode VisitPointerType(IPointerTypeSymbol pointerTypeSymbol)
        {
            return new PointerTypeNameNode(
                elementType: Visit(pointerTypeSymbol.PointedAtType),
                asteriskToken: NameFactory.MissingToken(NameTokenKind.Asterisk));
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
                dotToken: NameFactory.MissingToken(NameTokenKind.Dot));
        }
    }
}