using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class CSharpQueryExtensions_Constraints
    {
        public static CSharpQuery<TResult> Allow<TResult>(this CSharpQuery<TResult> query)
            where TResult : ISymbol
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var result = context.Locate<TResult>(query.Name);
                var storage = context.Provider.GetRequiredService<AllowedSymbolStorage>();
                foreach(var symbol in result)
                {
                    storage.Allow(symbol);
                }
            });
            return query;
        }

        public static CSharpQuery<TResult> Exists<TResult>(this CSharpQuery<TResult> query)
            where TResult : ISymbol
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var result = context.Locate<TResult>(query.Name);
                if (!result.IsEmpty)
                {
                    return;
                }

                var current = query.Name;
                ImmutableArray<ISymbol> parents = default;
                while (parents.IsDefaultOrEmpty && current != null)
                {
                    current = query.Name.GetLogicalParent();
                    parents = context.Locate(current);
                }

                if (parents.IsDefaultOrEmpty)
                {
                    context.Provider.GetRequiredService<IValidationReporter>()
                        .Report($"Symbol '{query.Name}' must exist.");
                    return;
                }

                foreach (var parent in parents)
                {
                    context.Report($"Symbol '{query.Name}' must exist.", parent);
                }
            });
            return query;
        }

        public static CSharpQuery<TResult> HasAccessibility<TResult>(this CSharpQuery<TResult> query, Accessibility accessibility)
            where TResult : ISymbol
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var result = context.Locate<TResult>(query.Name);
                foreach (var symbol in result)
                {
                    if (!accessibility.HasFlag(symbol.DeclaredAccessibility.ToUnitAccessibility()))
                    {
                        context.Report(
                            message: $"Symbol '{symbol}' has to declare accessibility '{accessibility}'.",
                            symbol: symbol);
                    }
                }
            });
            return query;
        }

        public static CSharpQuery<ITypeSymbol> HasBaseType<TBase>(this CSharpQuery<ITypeSymbol> query)
            where TBase : class
        {
            return query.HasBaseType(query.Unit.GetMetaName<TBase>());
        }

        public static CSharpQuery<ITypeSymbol> HasBaseType(this CSharpQuery<ITypeSymbol> query, string typeName)
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var baseType = context.Locate<ITypeSymbol>(NameNode.Parse(typeName)).Single();
                var result = context.Locate<ITypeSymbol>(query.Name);
                foreach (var typeSymbol in result)
                {
                    if (!typeSymbol.BaseType.Equals(baseType))
                    {
                        context.Report(
                            message: $"Type '{typeSymbol}' must have '{baseType}' as its base type.",
                            symbol: typeSymbol);
                    }
                }
            });
            return query;
        }

        public static CSharpQuery<IPropertySymbol> HasGetter(
            this CSharpQuery<IPropertySymbol> query,
            Accessibility accessibility = Accessibility.Public)
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var result = context.Locate<IPropertySymbol>(query.Name);
                foreach (var property in result)
                {
                    if (property.GetMethod == null)
                    {
                        context.Report(
                            message: "Property must have a getter.",
                            symbol: property);
                    }
                    else if (!accessibility.HasFlag(property.GetMethod.DeclaredAccessibility.ToUnitAccessibility()))
                    {
                        context.Report(
                            message: $"Getter must have '{accessibility}' accessibility.",
                            symbol: property.GetMethod);
                    }
                }
            });
            return query;
        }

        public static CSharpQuery<IPropertySymbol> HasSetter(
            this CSharpQuery<IPropertySymbol> query,
            Accessibility accessibility = Accessibility.Public)
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var result = context.Locate<IPropertySymbol>(query.Name);
                foreach (var property in result)
                {
                    if (property.SetMethod == null)
                    {
                        context.Report(
                            message: "Property must have a setter.",
                            symbol: property);
                    }
                    else if (!accessibility.HasFlag(property.SetMethod.DeclaredAccessibility.ToUnitAccessibility()))
                    {
                        context.Report(
                            message: $"Setter must have '{accessibility}' accessibility.",
                            symbol: property.SetMethod);
                    }
                }
            });
            return query;
        }

        public static CSharpQuery<ITypeSymbol> Implements<TInterface>(this CSharpQuery<ITypeSymbol> query)
        {
            return query.Implements(query.Unit.GetMetaName<TInterface>());
        }

        public static CSharpQuery<ITypeSymbol> Implements(this CSharpQuery<ITypeSymbol> query, string typeName)
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var interfaceSymbol = context.Locate<ITypeSymbol>(NameNode.Parse(typeName)).Single();
                var result = context.Locate<ITypeSymbol>(NameNode.Parse(typeName));
                foreach (var typeSymbol in result)
                {
                    if (!typeSymbol.AllInterfaces.Contains(interfaceSymbol))
                    {
                        context.Report(
                            message: $"Type '{typeSymbol}' must implement '{interfaceSymbol}'.",
                            symbol: typeSymbol);
                    }
                }
            }, false);
            return query;
        }

        public static CSharpQuery<IFieldSymbol> IsOfType<TType>(this CSharpQuery<IFieldSymbol> query)
        {
            return query.IsOfType(query.Unit.GetMetaName<TType>());
        }

        public static CSharpQuery<IFieldSymbol> IsOfType(this CSharpQuery<IFieldSymbol> query, string typeName)
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var type = context.Locate<IFieldSymbol>(NameNode.Parse(typeName)).Single();
                var result = context.Locate<IFieldSymbol>(query.Name);
                foreach (var field in result)
                {
                    if (!field.Type.Equals(type))
                    {
                        context.Report(
                            message: $"Field '{field}' must be of type '{type}'.",
                            symbol: field);
                    }
                }
            });
            return query;
        }

        public static CSharpQuery<IPropertySymbol> IsOfType<TType>(this CSharpQuery<IPropertySymbol> query)
        {
            return query.IsOfType(query.Unit.GetMetaName<TType>());
        }

        public static CSharpQuery<IPropertySymbol> IsOfType(this CSharpQuery<IPropertySymbol> query, string typeName)
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var type = context.Locate<ITypeSymbol>(NameNode.Parse(typeName)).Single();
                var result = context.Locate<IPropertySymbol>(query.Name);
                foreach (var property in result)
                {
                    if (!property.Type.Equals(type))
                    {
                        context.Report(
                            message: $"Property '{property}' must be of type '{type}'.",
                            symbol: property);
                    }
                }
            });
            return query;
        }

        public static CSharpQuery<IFieldSymbol> IsReadonly(this CSharpQuery<IFieldSymbol> query)
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var result = context.Locate<IFieldSymbol>(query.Name);
                foreach (var field in result)
                {
                    if (!field.IsReadOnly)
                    {
                        context.Report(
                            message: $"Field '{field}' must be readonly.",
                            symbol: field);
                    }
                }
            });
            return query;
        }

        public static CSharpQuery<ITypeSymbol> IsTypeKind(this CSharpQuery<ITypeSymbol> query, TypeKind typeKind)
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var result = context.Locate<ITypeSymbol>(query.Name);
                foreach (var symbol in result)
                {
                    if (!typeKind.HasFlag(symbol.TypeKind.ToUnitTypeKind()))
                    {
                        context.Report(
                            message: $"Type '{symbol}' has to be a '{typeKind}'.",
                            symbol: symbol);
                    }
                }
            });
            return query;
        }

        public static CSharpQuery<IMethodSymbol> Returns<TType>(this CSharpQuery<IMethodSymbol> query)
        {
            return query.Returns(query.Unit.GetMetaName<TType>());
        }

        public static CSharpQuery<IMethodSymbol> Returns(this CSharpQuery<IMethodSymbol> query, string typeName)
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var type = context.Locate<ITypeSymbol>(NameNode.Parse(typeName)).Single();
                var result = context.Locate<IMethodSymbol>(query.Name);
                foreach (var method in result)
                {
                    if (!method.ReturnType.Equals(type))
                    {
                        context.Report(
                            message: $"Method '{method}' must return a '{type}'.",
                            symbol: method);
                    }
                }
            });
            return query;
        }
    }
}