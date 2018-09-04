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
        public static CSharpQuery<TResult> HasAttribute<TResult>(this CSharpQuery<TResult> query, Type attributeType)
            where TResult : ISymbol
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var converter = context.Provider.GetRequiredService<IMemberInfoConverter>();
                var attributeClass = (INamedTypeSymbol)converter.Convert(attributeType);
                var result = context.Locate<TResult>(query.Name);
                foreach (var symbol in result)
                {
                    if (!symbol.GetAttributes().Any(a => a.AttributeClass.Equals(attributeClass)))
                    {
                        context.Report(
                            message: $"Symbol '{symbol}' must have a '{attributeClass}' attribute.",
                            symbol: symbol);
                    }
                }
            }, false);
            return query;
        }

        public static CSharpQuery<TResult> HasAttribute<TResult>(this CSharpQuery<TResult> query, Attribute expected)
            where TResult : ISymbol
        {
            var attributeType = expected.GetType();
            query.Unit.AddDelegateConstraint(context =>
            {
                var converter = context.Provider.GetRequiredService<IMemberInfoConverter>();
                var extractor = context.Provider.GetRequiredService<ITypedAttributeExtractor>();
                var result = context.Locate<TResult>(query.Name);

                // TODO: Use something else for object comparison. This generates errors that are too broad.
                var propertyComparer = context.Provider.GetRequiredService<PropertyEqualityComparer>();

                foreach (var symbol in result)
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
            }, false);
            return query;
        }

        public static CSharpQuery<TResult> HasNoAttribute<TResult>(this CSharpQuery<TResult> query, Type attributeType)
            where TResult : ISymbol
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var converter = context.Provider.GetRequiredService<IMemberInfoConverter>();
                var attributeClass = (INamedTypeSymbol)converter.Convert(attributeType);
                var result = context.Locate<TResult>(query.Name);
                foreach (var symbol in result)
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
            }, false);
            return query;
        }
    }
}