using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DotvvmAcademy.Meta
{
    public class ReflectionSymbolVisitor : SymbolVisitor<IEnumerable<MemberInfo>>
    {
        private readonly IEnumerable<Assembly> assemblies;

        public ReflectionSymbolVisitor(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = assemblies;
        }

        public override IEnumerable<MemberInfo> DefaultVisit(ISymbol symbol)
        {
            throw new NotSupportedException($"ISymbol type \"{symbol.GetType()}\" is not supported.");
        }

        public override IEnumerable<MemberInfo> VisitArrayType(IArrayTypeSymbol symbol)
        {
            return Visit(symbol.ElementType)
                .OfType<Type>()
                .Select(t => t.MakeArrayType(symbol.Rank));
        }

        public override IEnumerable<MemberInfo> VisitEvent(IEventSymbol symbol)
        {
            return Visit(symbol.ContainingType)
                .OfType<Type>()
                .Select(t => t.GetEvent(symbol.Name, BindingFlags.Public | BindingFlags.NonPublic))
                .Where(p => p != null);
        }

        public override IEnumerable<MemberInfo> VisitField(IFieldSymbol symbol)
        {
            return Visit(symbol.ContainingType)
                .OfType<Type>()
                .Select(t => t.GetField(symbol.Name, BindingFlags.Public | BindingFlags.NonPublic))
                .Where(p => p != null);
        }

        public override IEnumerable<MemberInfo> VisitMethod(IMethodSymbol symbol)
        {
            var arguments = symbol.Parameters.Select(p => Visit(p.Type).OfType<Type>().First())
                .ToArray();
            return Visit(symbol.ContainingType)
                .OfType<Type>()
                .Select(t => t.GetMethod(
                    name: symbol.Name,
                    bindingAttr: BindingFlags.Public | BindingFlags.NonPublic,
                    binder: Type.DefaultBinder,
                    types: arguments,
                    modifiers: Array.Empty<ParameterModifier>()));
        }

        public override IEnumerable<MemberInfo> VisitNamedType(INamedTypeSymbol symbol)
        {
            if (symbol.IsConstructedType())
            {
                var arguments = symbol.TypeArguments.Select(a => Visit(a).OfType<Type>().First())
                    .ToArray();

                return Visit(symbol.ConstructedFrom)
                    .OfType<Type>()
                    .Select(t => t.MakeGenericType(arguments));
            }
            else if (symbol.ContainingType != null)
            {
                return Visit(symbol.ContainingType)
                    .OfType<Type>()
                    .Select(t => t.GetNestedType(symbol.Name, BindingFlags.Public | BindingFlags.NonPublic));
            }
            else
            {
                return assemblies.GetTypes(GetFullName(symbol));
            }
        }

        public override IEnumerable<MemberInfo> VisitPointerType(IPointerTypeSymbol symbol)
        {
            return Visit(symbol.PointedAtType)
                .OfType<Type>()
                .Select(t => t.MakePointerType());
        }

        public override IEnumerable<MemberInfo> VisitProperty(IPropertySymbol symbol)
        {
            return Visit(symbol.ContainingType)
                .OfType<Type>()
                .Select(t => t.GetProperty(symbol.Name, BindingFlags.Public | BindingFlags.NonPublic))
                .Where(p => p != null);
        }

        private string GetFullName(INamedTypeSymbol symbol)
        {
            // symbol must be a top-level type
            // type arguments are not supported
            string GetNamespaceName(INamespaceSymbol @namespace)
            {
                if (@namespace.ContainingNamespace.IsGlobalNamespace)
                {
                    return @namespace.Name;
                }
                else
                {
                    return $"{GetNamespaceName(@namespace.ContainingNamespace)}.{@namespace.Name}";
                }
            }

            var sb = new StringBuilder();
            if (!symbol.ContainingNamespace.IsGlobalNamespace)
            {
                sb.Append(GetNamespaceName(symbol.ContainingNamespace));
                sb.Append('.');
            }
            sb.Append(symbol.Name);
            if (symbol.Arity > 0)
            {
                sb.Append('`');
                sb.Append(symbol.Arity);
            }
            return sb.ToString();
        }
    }
}