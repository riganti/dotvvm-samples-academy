﻿using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public class SymbolLocator
    {
        private readonly ImmutableArray<IAssemblySymbol> assemblies;
        private readonly CSharpCompilation compilation;

        public SymbolLocator(CSharpCompilation compilation)
        {
            this.compilation = compilation;
            assemblies = compilation.References
                .Select(r => (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(r))
                .ToImmutableArray();
        }

        public ImmutableArray<ISymbol> Locate(string name)
        {
            var lexer = new NameLexer(name);
            var parser = new NameParser(lexer);
            return Locate(parser.Parse());
        }

        public ImmutableArray<ISymbol> Locate(NameNode node)
        {
            return Visit(node).ToImmutableArray();
        }

        private IEnumerable<ISymbol> Visit(NameNode node)
        {
            switch (node)
            {
                case IdentifierNameNode identifier:
                    return VisitIdentifier(identifier);

                case GenericNameNode generic:
                    return VisitGeneric(generic);

                case PredefinedTypeNameNode predefinedType:
                    return VisitPredefinedType(predefinedType);

                case QualifiedNameNode qualified:
                    return VisitQualified(qualified);

                case NestedTypeNameNode nestedType:
                    return VisitNestedType(nestedType);

                case ConstructedTypeNameNode constructedType:
                    return VisitConstructedType(constructedType);

                case PointerTypeNameNode pointerType:
                    return VisitPointerType(pointerType);

                case ArrayTypeNameNode arrayType:
                    return VisitArrayType(arrayType);

                case MemberNameNode member:
                    return VisitMember(member);

                default:
                    throw new NotImplementedException($"{nameof(NameNode)} type '{node.GetType()}' is not supported.");
            }
        }

        private IEnumerable<ISymbol> VisitArrayType(ArrayTypeNameNode arrayType)
        {
            var rank = arrayType.CommaTokens.Length + 1;
            return Visit(arrayType.ElementType)
                .OfType<ITypeSymbol>()
                .Select(t => compilation.CreateArrayTypeSymbol(t, rank));
        }

        private IEnumerable<ISymbol> VisitConstructedType(ConstructedTypeNameNode constructedType)
        {
            var arguments = constructedType.TypeArgumentList.Arguments
                .Select(a => Visit(a).Single())
                .OfType<ITypeSymbol>()
                .ToArray();
            return Visit(constructedType.UnboundTypeName)
                .OfType<INamedTypeSymbol>()
                .Select(t => t.Construct(arguments));
        }

        private IEnumerable<ISymbol> VisitGeneric(GenericNameNode generic)
        {
            return assemblies.SelectMany(a => VisitGeneric(a.GlobalNamespace, generic));
        }

        private IEnumerable<ISymbol> VisitGeneric(INamespaceSymbol namespaceSymbol, GenericNameNode generic)
        {
            var arity = int.Parse(generic.ArityToken.ToString());
            return namespaceSymbol.GetTypeMembers(generic.IdentifierToken.ToString(), arity);
        }

        private IEnumerable<ISymbol> VisitIdentifier(IdentifierNameNode identifier)
        {
            return assemblies
                .SelectMany(a => VisitIdentifier(a.GlobalNamespace, identifier));
        }

        private IEnumerable<ISymbol> VisitIdentifier(INamespaceSymbol namespaceSymbol, IdentifierNameNode identifier)
        {
            return namespaceSymbol.GetMembers(identifier.IdentifierToken.ToString())
                .Where(s => s is INamespaceSymbol || (s is INamedTypeSymbol typeSymbol && typeSymbol.Arity == 0));
        }

        private IEnumerable<ISymbol> VisitMember(MemberNameNode member)
        {
            return Visit(member.Type)
                .OfType<ITypeSymbol>()
                .SelectMany(t => t.GetMembers(member.Member.IdentifierToken.ToString()));
        }

        private IEnumerable<ISymbol> VisitNestedType(NestedTypeNameNode nestedType)
        {
            return Visit(nestedType.Left)
                .OfType<ITypeSymbol>()
                .SelectMany(t => t.GetTypeMembers(nestedType.Right.IdentifierToken.ToString()));
        }

        private IEnumerable<ISymbol> VisitPointerType(PointerTypeNameNode pointerType)
        {
            return Visit(pointerType.ElementType)
                .Cast<ITypeSymbol>()
                .Select(t => compilation.CreatePointerTypeSymbol(t));
        }

        private IEnumerable<ISymbol> VisitPredefinedType(PredefinedTypeNameNode predefinedType)
        {
            INamedTypeSymbol type;
            switch (predefinedType.Kind)
            {
                case NameNodeKind.BoolType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.Bool);
                    break;

                case NameNodeKind.ByteType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.Byte);
                    break;

                case NameNodeKind.SByteType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.SByte);
                    break;

                case NameNodeKind.IntType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.Int);
                    break;

                case NameNodeKind.UIntType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.UInt);
                    break;

                case NameNodeKind.ShortType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.Short);
                    break;

                case NameNodeKind.UShortType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.UShort);
                    break;

                case NameNodeKind.LongType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.Long);
                    break;

                case NameNodeKind.ULongType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.ULong);
                    break;

                case NameNodeKind.FloatType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.Float);
                    break;

                case NameNodeKind.DoubleType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.Double);
                    break;

                case NameNodeKind.DecimalType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.Decimal);
                    break;

                case NameNodeKind.StringType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.String);
                    break;

                case NameNodeKind.CharType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.Char);
                    break;

                case NameNodeKind.ObjectType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.Object);
                    break;

                case NameNodeKind.VoidType:
                    type = compilation.GetTypeByMetadataName(WellKnownTypes.Void);
                    break;

                default:
                    throw new NotSupportedException($"{nameof(NameNodeKind)} '{predefinedType.Kind}' is not " +
                        $"a supported prefined type kind.");
            }
            return ImmutableArray.Create(type);
        }

        private IEnumerable<ISymbol> VisitQualified(QualifiedNameNode qualified)
        {
            return Visit(qualified.Left)
                .OfType<INamespaceSymbol>()
                .SelectMany(n => VisitSimple(n, qualified.Right));
        }

        private IEnumerable<ISymbol> VisitSimple(INamespaceSymbol namespaceSymbol, SimpleNameNode simple)
        {
            switch (simple)
            {
                case IdentifierNameNode identifier:
                    return VisitIdentifier(namespaceSymbol, identifier);

                case GenericNameNode generic:
                    return VisitGeneric(namespaceSymbol, generic);

                default:
                    throw new NotSupportedException($"{nameof(SimpleNameNode)} type '{simple.GetType()}' is " +
                        $"not supported.");
            }
        }
    }
}