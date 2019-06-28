using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public class AttributeExtractor : IAttributeExtractor
    {
        private readonly ITypedConstantExtractor constantExtractor;
        private readonly MetaConverter converter;

        public AttributeExtractor(MetaConverter converter, ITypedConstantExtractor constantExtractor)
        {
            this.converter = converter;
            this.constantExtractor = constantExtractor;
        }

        public Attribute Extract(AttributeData attributeData)
        {
            var attributeType = (Type)converter.ToReflection(attributeData.AttributeClass).Single();
            return Instantiate(attributeType, attributeData);
        }

        public IEnumerable<Attribute> Extract(Type attributeType, ISymbol symbol)
        {
            var attributeClass = (INamedTypeSymbol)converter.ToRoslyn(attributeType).Single();
            return symbol.GetAttributes()
                .Where(t => t.AttributeClass.Equals(attributeClass))
                .Select(a => Instantiate(attributeType, a));
        }

        public IEnumerable<AttributeData> ExtractAttributeData(Type attributeType, ISymbol symbol)
        {
            var attributeClass = (INamedTypeSymbol)converter.ToRoslyn(attributeType).Single();
            return symbol.GetAttributes()
                .Where(a => a.AttributeClass.Equals(attributeClass));
        }

        public bool HasAttribute(Type attributeType, ISymbol symbol)
        {
            var attributeClass = (INamedTypeSymbol)converter.ToRoslyn(attributeType).Single();
            return symbol.GetAttributes().Any(a => a.AttributeClass.Equals(attributeClass));
        }

        protected Attribute Instantiate(Type attributeType, AttributeData attributeData)
        {
            var arguments = attributeData.ConstructorArguments
                .Select(constantExtractor.Extract)
                .ToArray();
            var instance = Activator.CreateInstance(attributeType, arguments);
            foreach (var namedArgument in attributeData.NamedArguments)
            {
                var property = attributeType.GetProperty(namedArgument.Key);
                var argument = constantExtractor.Extract(namedArgument.Value);
                property.SetValue(instance, argument);
            }
            return (Attribute)instance;
        }
    }
}