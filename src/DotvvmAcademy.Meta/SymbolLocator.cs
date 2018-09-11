using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public class SymbolLocator : ISymbolLocator
    {
        private readonly ICSharpCompilationAccessor compilationAccessor;

        public SymbolLocator(ICSharpCompilationAccessor compilationAccessor)
        {
            this.compilationAccessor = compilationAccessor;
        }

        public ImmutableArray<ISymbol> Locate(NameNode node)
        {
            return Visit(node)
                .ToImmutableArray();
        }

        private IEnumerable<INamespaceSymbol> GetGlobalNamespaces()
        {
            yield return compilationAccessor.Compilation.GlobalNamespace;

            foreach (var reference in compilationAccessor.Compilation.References)
            {
                var symbol = compilationAccessor.Compilation.GetAssemblyOrModuleSymbol(reference);
                switch (symbol)
                {
                    case IAssemblySymbol assembly:
                        yield return assembly.GlobalNamespace;
                        break;

                    case IModuleSymbol module:
                        yield return module.GlobalNamespace;
                        break;

                    default:
                        throw new NotSupportedException($"Compilation.GetAssemblyOrModuleSymbol returned '{symbol.GetType()}'.");
                }
            }
        }

        private INamedTypeSymbol GetTypeByMetadataName(string metadataName)
        {
            return compilationAccessor.Compilation.GetTypeByMetadataName(metadataName);
        }

        private IEnumerable<ISymbol> Visit(NameNode node)
        {
            IEnumerable<ISymbol> result;
            switch (node)
            {
                case IdentifierNameNode identifier:
                    result = VisitIdentifier(identifier);
                    break;

                case GenericNameNode generic:
                    result = VisitGeneric(generic);
                    break;

                case QualifiedNameNode qualified:
                    result = VisitQualified(qualified);
                    break;

                case NestedTypeNameNode nestedType:
                    result =  VisitNestedType(nestedType);
                    break;

                case ConstructedTypeNameNode constructedType:
                    result = VisitConstructedType(constructedType);
                    break;

                case PointerTypeNameNode pointerType:
                    result = VisitPointerType(pointerType);
                    break;

                case ArrayTypeNameNode arrayType:
                    result = VisitArrayType(arrayType);
                    break;

                case MemberNameNode member:
                    result = VisitMember(member);
                    break;

                default:
                    throw new NotImplementedException($"{nameof(NameNode)} type '{node.GetType()}' is not supported.");
            }
            // TODO: Fix problem with duplicate global namespace
            return result.Distinct();
        }

        private IEnumerable<ISymbol> VisitArrayType(ArrayTypeNameNode arrayType)
        {
            var rank = arrayType.CommaTokens.Length + 1;
            return Visit(arrayType.ElementType)
                .OfType<ITypeSymbol>()
                .Select(t => compilationAccessor.Compilation.CreateArrayTypeSymbol(t, rank));
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
            return GetGlobalNamespaces()
                .SelectMany(n => VisitGeneric(n, generic));
        }

        private IEnumerable<ISymbol> VisitGeneric(INamespaceSymbol namespaceSymbol, GenericNameNode generic)
        {
            var arity = int.Parse(generic.ArityToken.ToString());
            return namespaceSymbol.GetTypeMembers(generic.IdentifierToken.ToString(), arity);
        }

        private IEnumerable<ISymbol> VisitIdentifier(IdentifierNameNode identifier)
        {
            return GetGlobalNamespaces()
                .SelectMany(n => VisitIdentifier(n, identifier));
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
                .Select(t => compilationAccessor.Compilation.CreatePointerTypeSymbol(t));
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