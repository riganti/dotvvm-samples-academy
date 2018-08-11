using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public class AttributeExtractor : IAttributeExtractor
    {
        private readonly ITypedConstantExtractor constantExtractor;
        private readonly ISymbolConverter symbolConverter;

        public AttributeExtractor(ITypedConstantExtractor constantExtractor, ISymbolConverter symbolConverter)
        {
            this.constantExtractor = constantExtractor;
            this.symbolConverter = symbolConverter;
        }

        public Attribute Extract(AttributeData attributeData)
        {
            var arguments = attributeData.ConstructorArguments
                .Select(constantExtractor.Extract)
                .ToArray();
            var attributeType = (Type)symbolConverter.Convert(attributeData.AttributeClass);
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