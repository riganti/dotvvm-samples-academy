using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class QueryConstraintExtensions
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

        public static IQuery<TResult> HasAccessibility<TResult>(
            this IQuery<TResult> query,
            Accessibility accessibility)
            where TResult : ISymbol
        {
            query.SetConstraint(nameof(HasAccessibility), context =>
            {
                foreach (var symbol in context.Result)
                {
                    if (!accessibility.HasFlag(symbol.DeclaredAccessibility.ToUnitAccessibility()))
                    {
                        context.GetReporter().ReportAllLocations(
                            message: $"Symbol '{symbol}' has to declare accessibility '{accessibility}'.",
                            symbol: symbol);
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
                        context.GetReporter().ReportAllLocations(
                            message: "Property must have a getter.",
                            symbol: property);
                    }
                    else if (!accessibility.HasFlag(property.GetMethod.DeclaredAccessibility.ToUnitAccessibility()))
                    {
                        context.GetReporter().ReportAllLocations(
                            message: $"Getter must have '{accessibility}' accessibility.",
                            symbol: property.GetMethod);
                    }
                }
            });
            return query;
        }

        public static IQuery<IMethodSymbol> HasParameters(this IQuery<IMethodSymbol> query, params string[] parameters)
        {
            return query.HasParameters(parameters.ToImmutableArray());
        }

        public static IQuery<IMethodSymbol> HasParameters(
            this IQuery<IMethodSymbol> query,
            ImmutableArray<string> parameters)
        {
            query.SetConstraint(nameof(HasParameters), context =>
            {
                var locator = context.Provider.GetRequiredService<SymbolLocator>();
                foreach (var method in context.Result)
                {
                    if (parameters.Length != method.Parameters.Length)
                    {
                        context.GetReporter().ReportAllLocations(
                            message: $"Method must have '{parameters.Length}' parameters.",
                            symbol: method);
                        continue;
                    }

                    for (int i = 0; i < method.Parameters.Length; i++)
                    {
                        var expectedParameter = locator.LocateSingle(parameters[i]);
                        if (expectedParameter != method.Parameters[i])
                        {
                            context.GetReporter().ReportAllLocations(
                                message: $"Parameters must be of type '{expectedParameter}'.",
                                symbol: method.Parameters[i]);
                        }
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
                        context.GetReporter().ReportAllLocations(
                            message: "Property must have a setter.",
                            symbol: property);
                    }
                    else if (!accessibility.HasFlag(property.SetMethod.DeclaredAccessibility.ToUnitAccessibility()))
                    {
                        context.GetReporter().ReportAllLocations(
                            message: $"Setter must have '{accessibility}' accessibility.",
                            symbol: property.SetMethod);
                    }
                }
            });
            return query;
        }

        public static IQuery<ITypeSymbol> IsTypeKind(
                    this IQuery<ITypeSymbol> query,
            TypeKind typeKind)
        {
            query.SetConstraint(nameof(IsTypeKind), context =>
            {
                foreach (var symbol in context.Result)
                {
                    if (!typeKind.HasFlag(symbol.TypeKind.ToUnitTypeKind()))
                    {
                        context.GetReporter().ReportAllLocations(
                            message: $"Type '{symbol}' has to be a '{typeKind}'.",
                            symbol: symbol);
                    }
                }
            });
            return query;
        }
    }
}