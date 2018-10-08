using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class AttributeExtractor : IAttributeExtractor
    {
        private readonly ITypedConstantExtractor constantExtractor;
        private readonly IMetaConverter<MemberInfo, ISymbol> memberInfoConverter;
        private readonly IMetaConverter<ISymbol, MemberInfo> symbolConverter;

        public AttributeExtractor(
            ITypedConstantExtractor constantExtractor,
            IMetaConverter<ISymbol, MemberInfo> symbolConverter,
            IMetaConverter<MemberInfo, ISymbol> memberInfoConverter)
        {
            this.constantExtractor = constantExtractor;
            this.symbolConverter = symbolConverter;
            this.memberInfoConverter = memberInfoConverter;
        }

        public Attribute Extract(AttributeData attributeData)
        {
            var attributeType = (Type)symbolConverter.Convert(attributeData.AttributeClass).Single();
            return Instantiate(attributeType, attributeData);
        }

        public IEnumerable<Attribute> Extract(Type attributeType, ISymbol symbol)
        {
            var attributeClass = (INamedTypeSymbol)memberInfoConverter.Convert(attributeType).Single();
            return symbol.GetAttributes()
                .Where(t => t.AttributeClass.Equals(attributeClass))
                .Select(a => Instantiate(attributeType, a));
        }

        public IEnumerable<AttributeData> ExtractAttributeData(Type attributeType, ISymbol symbol)
        {
            var attributeClass = (INamedTypeSymbol)memberInfoConverter.Convert(attributeType).Single();
            return symbol.GetAttributes()
                .Where(a => a.AttributeClass.Equals(attributeClass));
        }

        public bool HasAttribute(Type attributeType, ISymbol symbol)
        {
            var attributeClass = (INamedTypeSymbol)memberInfoConverter.Convert(attributeType).Single();
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