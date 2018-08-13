using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class QueryExtensions_Constraints
    {
        public static IQuery<TResult> Allow<TResult>(this IQuery<TResult> query)
            where TResult : ISymbol
        {
            query.SetConstraint(nameof(Allow), context =>
            {
                var storage = context.Provider.GetRequiredService<AllowedSymbolStorage>();
                foreach (var symbol in context.Result)
                {
                    storage.Allow(symbol);
                }
            });
            return query;
        }

        public static IQuery<TResult> HasAccessibility<TResult>(this IQuery<TResult> query, Accessibility accessibility)
            where TResult : ISymbol
        {
            query.SetConstraint(nameof(HasAccessibility), context =>
            {
                foreach (var symbol in context.Result)
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

        public static IQuery<ITypeSymbol> HasBaseType<TBase>(this IQuery<ITypeSymbol> query)
            where TBase : class
        {
            return query.HasBaseType(query.Unit.GetMetaName<TBase>());
        }

        public static IQuery<ITypeSymbol> HasBaseType(this IQuery<ITypeSymbol> query, string typeName)
        {
            query.SetConstraint(nameof(HasBaseType), context =>
            {
                var baseType = context.LocateSymbol<ITypeSymbol, ITypeSymbol>(typeName);
                foreach (var typeSymbol in context.Result)
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

        public static IQuery<IPropertySymbol> HasGetter(
            this IQuery<IPropertySymbol> query,
            Accessibility accessibility = Accessibility.Public)
        {
            query.SetConstraint(nameof(HasGetter), context =>
            {
                foreach (var property in context.Result)
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

        public static IQuery<IPropertySymbol> HasSetter(
            this IQuery<IPropertySymbol> query,
            Accessibility accessibility = Accessibility.Public)
        {
            query.SetConstraint(nameof(HasSetter), context =>
            {
                foreach (var property in context.Result)
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

        public static IQuery<ITypeSymbol> Implements<TInterface>(this IQuery<ITypeSymbol> query)
        {
            return query.Implements(query.Unit.GetMetaName<TInterface>());
        }

        public static IQuery<ITypeSymbol> Implements(this IQuery<ITypeSymbol> query, string typeName)
        {
            query.SetConstraint($"{nameof(Implements)}_{typeName}", context =>
            {
                var interfaceSymbol = context.LocateSymbol<ITypeSymbol, ITypeSymbol>(typeName);
                foreach (var typeSymbol in context.Result)
                {
                    if (!typeSymbol.AllInterfaces.Contains(interfaceSymbol))
                    {
                        context.Report(
                            message: $"Type '{typeSymbol}' must implement '{interfaceSymbol}'.",
                            symbol: typeSymbol);
                    }
                }
            });
            return query;
        }

        public static IQuery<IFieldSymbol> IsOfType<TType>(this IQuery<IFieldSymbol> query)
        {
            return query.IsOfType(query.Unit.GetMetaName<TType>());
        }

        public static IQuery<IFieldSymbol> IsOfType(this IQuery<IFieldSymbol> query, string typeName)
        {
            query.SetConstraint(nameof(IsOfType), context =>
            {
                var type = context.LocateSymbol<IFieldSymbol, ITypeSymbol>(typeName);
                foreach (var field in context.Result)
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

        public static IQuery<IPropertySymbol> IsOfType<TType>(this IQuery<IPropertySymbol> query)
        {
            return query.IsOfType(query.Unit.GetMetaName<TType>());
        }

        public static IQuery<IPropertySymbol> IsOfType(this IQuery<IPropertySymbol> query, string typeName)
        {
            query.SetConstraint(nameof(IsOfType), context =>
            {
                var type = context.LocateSymbol<IPropertySymbol, ITypeSymbol>(typeName);
                foreach (var property in context.Result)
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

        public static IQuery<IFieldSymbol> IsReadonly(this IQuery<IFieldSymbol> query)
        {
            query.SetConstraint(nameof(IsReadonly), context =>
            {
                foreach (var field in context.Result)
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

        public static IQuery<ITypeSymbol> IsTypeKind(this IQuery<ITypeSymbol> query, TypeKind typeKind)
        {
            query.SetConstraint(nameof(IsTypeKind), context =>
            {
                foreach (var symbol in context.Result)
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

        public static IQuery<IMethodSymbol> Returns<TType>(this IQuery<IMethodSymbol> query)
        {
            return query.Returns(query.Unit.GetMetaName<TType>());
        }

        public static IQuery<IMethodSymbol> Returns(this IQuery<IMethodSymbol> query, string typeName)
        {
            query.SetConstraint(nameof(Returns), context =>
            {
                var type = context.LocateSymbol<IMethodSymbol, ITypeSymbol>(typeName);
                foreach (var method in context.Result)
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