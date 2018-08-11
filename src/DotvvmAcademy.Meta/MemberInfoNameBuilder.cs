using DotvvmAcademy.Meta.Syntax;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class MemberInfoNameBuilder : IMemberInfoNameBuilder
    {
        public NameNode Build(MemberInfo memberInfo)
        {
            return Visit(memberInfo);
        }

        private IdentifierNameNode GetIdentifier(MemberInfo memberInfo)
        {
            return new IdentifierNameNode(NameFactory.IdentifierToken(memberInfo.Name));
        }

        private NameNode GetQualifiedName(string qualifiedName)
        {
            Debug.Assert(!string.IsNullOrEmpty(qualifiedName));
            var segments = qualifiedName.Split('.');
            NameNode name = new IdentifierNameNode(NameFactory.IdentifierToken(segments[0]));
            for (var i = 1; i < segments.Length; i++)
            {
                name = new QualifiedNameNode(
                    left: name,
                    right: new IdentifierNameNode(NameFactory.IdentifierToken(segments[i])),
                    dotToken: NameFactory.MissingToken(NameTokenKind.Dot));
            }
            return name;
        }

        private SimpleNameNode GetSimpleName(MemberInfo memberInfo)
        {
            if (memberInfo is Type type && type.GenericTypeArguments.Length > 0)
            {
                return new GenericNameNode(
                    identifierToken: NameFactory.IdentifierToken(type.Name),
                    backtickToken: NameFactory.MissingToken(NameTokenKind.Backtick),
                    arityToken: NameFactory.NumericLiteralToken(type.GenericTypeArguments.Length));
            }

            return GetIdentifier(memberInfo);
        }

        private NameNode Visit(MemberInfo memberInfo)
        {
            switch (memberInfo)
            {
                case Type arrayType when arrayType.IsArray:
                    return VisitArrayType(arrayType);

                case Type pointerType when pointerType.IsPointer:
                    return VisitPointerType(pointerType);

                case Type constructedType when constructedType.IsConstructedGenericType:
                    return VisitConstructedType(constructedType);

                case Type nestedType when nestedType.IsNested:
                    return VisitNestedType(nestedType);

                case Type topLevelType:
                    return VisitTopLevelType(topLevelType);

                default:
                    return VisitMember(memberInfo);
            }
        }

        private NameNode VisitArrayType(Type arrayType)
        {
            return new ArrayTypeNameNode(
                elementType: Visit(arrayType.GetElementType()),
                openBracketToken: NameFactory.MissingToken(NameTokenKind.OpenBracket),
                closeBracketToken: NameFactory.MissingToken(NameTokenKind.CloseBracket),
                commaTokens: NameFactory.MissingTokens(NameTokenKind.Comma, arrayType.GetArrayRank()));
        }

        private NameNode VisitConstructedType(Type constructedType)
        {
            var typeArguments = constructedType.GetGenericArguments()
                .Select(a => Visit(a))
                .ToImmutableArray();
            var typeArgumentList = new TypeArgumentListNode(
                arguments: typeArguments,
                commaTokens: NameFactory.MissingTokens(NameTokenKind.Comma, typeArguments.Length - 1),
                openBracketToken: NameFactory.MissingToken(NameTokenKind.OpenBracket),
                closeBracketToken: NameFactory.MissingToken(NameTokenKind.CloseBracket));
            return new ConstructedTypeNameNode(
                unboundTypeName: Visit(constructedType.GetGenericTypeDefinition()),
                typeArgumentList: typeArgumentList);
        }

        private NameNode VisitMember(MemberInfo memberInfo)
        {
            return new MemberNameNode(
                type: Visit(memberInfo.DeclaringType),
                member: GetIdentifier(memberInfo),
                colonColonToken: NameFactory.MissingToken(NameTokenKind.ColonColon));
        }

        private NameNode VisitNestedType(Type nestedType)
        {
            return new NestedTypeNameNode(
                left: Visit(nestedType.DeclaringType),
                right: GetSimpleName(nestedType),
                plusToken: NameFactory.MissingToken(NameTokenKind.Plus));
        }

        private NameNode VisitPointerType(Type pointerType)
        {
            return new PointerTypeNameNode(
                elementType: Visit(pointerType.GetElementType()),
                asteriskToken: NameFactory.MissingToken(NameTokenKind.Asterisk));
        }

        private NameNode VisitTopLevelType(Type type)
        {
            var right = GetSimpleName(type);
            if (string.IsNullOrEmpty(type.Namespace))
            {
                return right;
            }

            return new QualifiedNameNode(
                left: GetQualifiedName(type.Namespace),
                right: right,
                dotToken: NameFactory.MissingToken(NameTokenKind.Dot));
        }
    }
}