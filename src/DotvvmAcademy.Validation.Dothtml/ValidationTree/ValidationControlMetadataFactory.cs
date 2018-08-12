using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationControlMetadataFactory
    {
        private readonly ITypedAttributeExtractor extractor;

        private readonly ConcurrentDictionary<ITypeSymbol, ValidationControlMetadata> cache
            = new ConcurrentDictionary<ITypeSymbol, ValidationControlMetadata>();

        private readonly IMemberInfoConverter memberInfoConverter;
        private readonly ValidationPropertyFactory propertyFactory;
        private readonly ValidationControlTypeFactory typeFactory;

        public ValidationControlMetadataFactory(
            ValidationControlTypeFactory typeFactory,
            ValidationPropertyFactory propertyFactory,
            ITypedAttributeExtractor extractor,
            IMemberInfoConverter memberInfoConverter)
        {
            this.typeFactory = typeFactory;
            this.propertyFactory = propertyFactory;
            this.extractor = extractor;
            this.memberInfoConverter = memberInfoConverter;
        }

        public ValidationControlMetadata Create(ITypeDescriptor typeDescriptor)
        {
            return Create(typeFactory.Create(typeDescriptor));
        }

        public ValidationControlMetadata Create(string metadataName)
        {
            return Create(typeFactory.Create(metadataName));
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
            return cache.GetOrAdd(symbol, s =>
            {
                var matchers = (from g in propertyFactory.GetGroups(symbol)
                                from prefix in g.Prefixes
                                select new PropertyGroupMatcher(prefix, g))
                                .ToImmutableArray();
                return new ValidationControlMetadata(
                 controlType: controlType,
                 changeAttributes: extractor.Extract<DataContextChangeAttribute>(symbol),
                 manipulationAttribute: extractor.Extract<DataContextStackManipulationAttribute>(symbol).SingleOrDefault(),
                 markupOptionsAttribute: extractor.Extract<ControlMarkupOptionsAttribute>(symbol).SingleOrDefault(),
                 properties: propertyFactory.GetProperties(symbol),
                 propertyGroupMatchers: matchers);
            });
        }
    }
}