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
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationControlMetadataFactory
    {
        private readonly ConcurrentDictionary<ITypeSymbol, ValidationControlMetadata> cache
            = new ConcurrentDictionary<ITypeSymbol, ValidationControlMetadata>();

        private readonly CSharpCompilation compilation;
        private readonly ValidationTypeDescriptorFactory descriptorFactory;
        private readonly AttributeExtractor extractor;
        private readonly ValidationPropertyDescriptorFactory propertyFactory;
        private readonly ValidationControlTypeFactory typeFactory;

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

        public ValidationControlMetadata Create(ITypeDescriptor typeDescriptor)
            => Create(typeFactory.Create(typeDescriptor));

        public ValidationControlMetadata Create(string metadataName)
            => Create(typeFactory.Create(metadataName));

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
            return cache.GetOrAdd(symbol, s =>
            {
                var matchers = (from g in propertyFactory.CreateGroups(symbol)
                                from prefix in g.Prefixes
                                select new PropertyGroupMatcher(prefix, g))
                                .ToImmutableArray();
                return new ValidationControlMetadata(
                 controlType: controlType,
                 changeAttributes: extractor.GetAttributes<DataContextChangeAttribute>(symbol),
                 manipulationAttribute: extractor.GetAttribute<DataContextStackManipulationAttribute>(symbol),
                 markupOptionsAttribute: extractor.GetAttribute<ControlMarkupOptionsAttribute>(symbol),
                 properties: propertyFactory.CreateProperties(symbol),
                 propertyGroupMatchers: matchers);
            });
        }
    }
}