using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public class TypedAttributeExtractor : AttributeExtractor, ITypedAttributeExtractor
    {
        private readonly ITypedConstantExtractor constantExtractor;
        private readonly IMemberInfoConverter memberInfoConverter;
        private readonly ISymbolConverter symbolConverter;

        public TypedAttributeExtractor(
            ITypedConstantExtractor constantExtractor,
            ISymbolConverter symbolConverter,
            IMemberInfoConverter memberInfoConverter)
            : base(constantExtractor, symbolConverter)
        {
            this.constantExtractor = constantExtractor;
            this.symbolConverter = symbolConverter;
            this.memberInfoConverter = memberInfoConverter;
        }

        public ImmutableArray<Attribute> Extract(Type attributeType, ISymbol symbol)
        {
            var attributeClass = (INamedTypeSymbol)memberInfoConverter.Convert(attributeType);
            var builder = ImmutableArray.CreateBuilder<Attribute>();
            return symbol.GetAttributes()
                .Where(t => t.AttributeClass.Equals(attributeClass))
                .Select(a => Instantiate(attributeType, a))
                .ToImmutableArray();
        }

        public ImmutableArray<AttributeData> ExtractRoslyn(Type attributeType, ISymbol symbol)
        {
            var attributeClass = (INamedTypeSymbol)memberInfoConverter.Convert(attributeType);
            return symbol.GetAttributes()
                .Where(a => a.AttributeClass.Equals(attributeClass))
                .ToImmutableArray();
        }

        public bool HasAttribute(Type attributeType, ISymbol symbol)
        {
            var attributeClass = (INamedTypeSymbol)memberInfoConverter.Convert(attributeType);
            return symbol.GetAttributes().Any(a => a.AttributeClass.Equals(attributeClass));
        }
    }
}