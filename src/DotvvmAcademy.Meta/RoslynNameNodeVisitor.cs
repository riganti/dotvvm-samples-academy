using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    internal class RoslynNameNodeVisitor : NameNodeVisitor<IEnumerable<ISymbol>>
    {
        private readonly Compilation compilation;

        public RoslynNameNodeVisitor(Compilation compilation)
        {
            this.compilation = compilation;
        }

        public override IEnumerable<ISymbol> VisitArrayType(ArrayTypeNameNode node)
        {
            var rank = node.CommaTokens.Length + 1;
            return Visit(node.ElementType)
                .OfType<ITypeSymbol>()
                .Select(t => compilation.CreateArrayTypeSymbol(t, rank));
        }

        public override IEnumerable<ISymbol> VisitConstructedType(ConstructedTypeNameNode node)
        {
            var arguments = node.Arguments
                .Select(a => Visit(a).First())
                .OfType<ITypeSymbol>()
                .ToArray();
            return Visit(node.UnboundTypeName)
                .OfType<INamedTypeSymbol>()
                .Select(t => t.Construct(arguments));
        }

        public override IEnumerable<ISymbol> VisitGeneric(GenericNameNode node)
        {
            return compilation.GetGlobalNamespaces()
                .SelectMany(n => VisitGeneric(node, n));
        }

        public IEnumerable<ISymbol> VisitGeneric(GenericNameNode node, INamespaceSymbol @namespace)
        {
            var arity = int.Parse(node.ArityToken.ToString());
            return @namespace.GetTypeMembers(node.IdentifierToken.ToString(), arity);
        }

        public override IEnumerable<ISymbol> VisitIdentifier(IdentifierNameNode node)
        {
            return compilation.GetGlobalNamespaces()
                .SelectMany(n => VisitIdentifier(node, n));
        }

        public IEnumerable<ISymbol> VisitIdentifier(IdentifierNameNode node, INamespaceSymbol @namespace)
        {
            return @namespace.GetMembers(node.IdentifierToken.ToString())
                .Where(s => s is INamespaceSymbol || (s is INamedTypeSymbol typeSymbol && typeSymbol.Arity == 0));
        }

        public override IEnumerable<ISymbol> VisitMember(MemberNameNode node)
        {
            return Visit(node.Type)
                .OfType<ITypeSymbol>()
                .SelectMany(t => t.GetMembers(node.Member.IdentifierToken.ToString()));
        }

        public override IEnumerable<ISymbol> VisitNestedType(NestedTypeNameNode node)
        {
            return Visit(node.Left)
                .OfType<ITypeSymbol>()
                .SelectMany(t => t.GetTypeMembers(node.Right.IdentifierToken.ToString()));
        }

        public override IEnumerable<ISymbol> VisitPointerType(PointerTypeNameNode node)
        {
            return Visit(node.ElementType)
                .Cast<ITypeSymbol>()
                .Select(t => compilation.CreatePointerTypeSymbol(t));
        }

        public override IEnumerable<ISymbol> VisitQualified(QualifiedNameNode node)
        {
            var namespaces = Visit(node.Left)
                .OfType<INamespaceSymbol>();
            switch (node.Right)
            {
                case IdentifierNameNode identifier:
                    return namespaces.SelectMany(n => VisitIdentifier(identifier, n));

                case GenericNameNode generic:
                    return namespaces.SelectMany(n => VisitGeneric(generic, n));

                default:
                    throw new NotSupportedException($"{nameof(SimpleNameNode)} type '{node.Right.GetType()}' is not supported.");
            }
        }
    }
}