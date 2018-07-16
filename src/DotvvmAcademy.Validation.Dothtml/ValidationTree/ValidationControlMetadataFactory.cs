using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControlMetadataFactory
    {
        private readonly ConcurrentDictionary<ITypeSymbol, ValidationControlMetadata> cache
            = new ConcurrentDictionary<ITypeSymbol, ValidationControlMetadata>();

        private readonly CSharpCompilation compilation;
        private readonly ValidationTypeDescriptorFactory descriptorFactory;
        private readonly AttributeExtractor extractor;
        private readonly ValidationControlTypeFactory typeFactory;
        private readonly ValidationPropertyDescriptorFactory propertyFactory;

        public ValidationControlMetadataFactory(
            CSharpCompilation compilation,
            ValidationTypeDescriptorFactory descriptorFactory,
            ValidationControlTypeFactory typeFactory,
            ValidationPropertyDescriptorFactory propertyFactory,
            AttributeExtractor extractor)
        {
            this.compilation = compilation;
            this.descriptorFactory = descriptorFactory;
            this.typeFactory = typeFactory;
            this.propertyFactory = propertyFactory;
            this.extractor = extractor;
        }

        public ValidationControlMetadata Create(IControlType controlType)
        {
            if (controlType is ValidationControlType validationType)
            {
                return Create(validationType.Type.TypeSymbol);
            }

            throw new ArgumentException($"IControlType '{controlType.GetType().Name}' is not supported.");
        }

        public ValidationControlMetadata Create(ITypeSymbol symbol)
        {
            var controlType = typeFactory.Create(symbol);

            return cache.GetOrAdd(symbol, s => new ValidationControlMetadata(
                controlType: controlType,
                changeAttributes: extractor.GetAttributes<DataContextChangeAttribute>(symbol),
                manipulationAttribute: extractor.GetAttribute<DataContextStackManipulationAttribute>(symbol),
                markupOptionsAttribute: extractor.GetAttribute<ControlMarkupOptionsAttribute>(symbol),
                properties: propertyFactory.CreateMany(symbol),
                propertyGroupMatchers: default(ImmutableArray<PropertyGroupMatcher>)));
        }
    }
}