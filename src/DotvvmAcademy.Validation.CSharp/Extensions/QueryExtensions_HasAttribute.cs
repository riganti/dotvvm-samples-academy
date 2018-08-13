using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class QueryExtensions_HasAttribute
    {
        public static IQuery<TResult> HasAttribute<TResult>(this IQuery<TResult> query, Type attributeType)
            where TResult : ISymbol
        {
            query.SetConstraint($"{nameof(HasAttribute)}_{attributeType}", context =>
            {
                var converter = context.Provider.GetRequiredService<IMemberInfoConverter>();
                var attributeClass = (INamedTypeSymbol)converter.Convert(attributeType);
                foreach (var symbol in context.Result)
                {
                    if (!symbol.GetAttributes().Any(a => a.AttributeClass.Equals(attributeClass)))
                    {
                        context.Report(
                            message: $"Symbol '{symbol}' must have a '{attributeClass}' attribute.",
                            symbol: symbol);
                    }
                }
            });
            return query;
        }

        public static IQuery<TResult> HasAttribute<TResult>(this IQuery<TResult> query, Attribute expected)
            where TResult : ISymbol
        {
            var attributeType = expected.GetType();
            query.SetConstraint($"{nameof(HasAttribute)}_{attributeType}", context =>
            {
                var converter = context.Provider.GetRequiredService<IMemberInfoConverter>();
                var extractor = context.Provider.GetRequiredService<ITypedAttributeExtractor>();

                // TODO: Use something else for object comparison. This generates errors that are too broad.
                var propertyComparer = context.Provider.GetRequiredService<PropertyEqualityComparer>();

                foreach (var symbol in context.Result)
                {
                    var attribute = extractor.Extract(attributeType, symbol).SingleOrDefault();
                    if (attribute == null)
                    {
                        context.Report(
                            message: $"Symbol '{symbol}' must have a '{attributeType}' attribute.",
                            symbol: symbol);
                    }
                    else if (!propertyComparer.Equals(expected, attribute))
                    {
                        context.Report(
                            message: $"The '{attributeType}' is not set correctly on '{symbol}'.",
                            symbol: symbol);
                    }
                }
            });
            return query;
        }

        public static IQuery<TResult> HasNoAttribute<TResult>(this IQuery<TResult> query, Type attributeType)
            where TResult : ISymbol
        {
            query.SetConstraint($"{nameof(HasNoAttribute)}_{attributeType}", context =>
            {
                var converter = context.Provider.GetRequiredService<IMemberInfoConverter>();
                var attributeClass = (INamedTypeSymbol)converter.Convert(attributeType);
                foreach (var symbol in context.Result)
                {
                    foreach (var attribute in symbol.GetAttributes())
                    {
                        if (attribute.AttributeClass.Equals(attributeClass))
                        {
                            context.Report(
                                message: $"Symbol '{symbol}' must have no '{attributeType}' attributes.",
                                symbol: symbol);
                        }
                    }
                }
            });
            return query;
        }
    }
}