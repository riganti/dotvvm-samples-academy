using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    internal class MetaSymbolVisitor : SymbolVisitor<NameNode>
    {
        public override NameNode DefaultVisit(ISymbol symbol)
        {
            throw new NotSupportedException($"Symbol of type \"{symbol.GetType()}\" is not supported.");
        }

        public override NameNode VisitArrayType(IArrayTypeSymbol symbol)
        {
            return NameFactory.ArrayType(Visit(symbol.ElementType), symbol.Rank);
        }

        public override NameNode VisitEvent(IEventSymbol symbol)
        {
            return VisitMember(symbol);
        }

        public override NameNode VisitField(IFieldSymbol symbol)
        {
            return VisitMember(symbol);
        }

        public override NameNode VisitMethod(IMethodSymbol symbol)
        {
            return VisitMember(symbol);
        }

        public override NameNode VisitNamedType(INamedTypeSymbol symbol)
        {
            if (symbol.IsConstructedType())
            {
                var typeArguments = symbol.TypeArguments.Select(Visit);
                return NameFactory.ConstructedType(Visit(symbol.ConstructedFrom), typeArguments);
            }
            else if (symbol.ContainingType != default)
            {
                return NameFactory.NestedType(Visit(symbol.ContainingType), symbol.Name, symbol.Arity);
            }
            else if (symbol.ContainingNamespace.IsGlobalNamespace)
            {
                return NameFactory.Simple(symbol.Name, symbol.Arity);
            }
            else
            {
                return NameFactory.Qualified(Visit(symbol.ContainingNamespace), symbol.Name, symbol.Arity);
            }
        }

        public override NameNode VisitNamespace(INamespaceSymbol symbol)
        {
            if (symbol.ContainingNamespace.IsGlobalNamespace)
            {
                return NameFactory.Identifier(symbol.Name);
            }
            else
            {
                return NameFactory.Qualified(VisitNamespace(symbol.ContainingNamespace), symbol.Name);
            }
        }

        public override NameNode VisitPointerType(IPointerTypeSymbol symbol)
        {
            return NameFactory.PointerType(Visit(symbol.PointedAtType));
        }

        public override NameNode VisitProperty(IPropertySymbol symbol)
        {
            return VisitMember(symbol);
        }

        private NameNode VisitMember(ISymbol symbol)
        {
            return NameFactory.Member(Visit(symbol.ContainingType), symbol.Name);
        }
    }
}