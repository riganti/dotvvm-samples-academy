using DotVVM.Framework.Binding;
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
    internal class ValidationPropertyDescriptorFactory
    {
        private readonly CSharpCompilation compilation;
        private readonly ValidationTypeDescriptorFactory descriptorFactory;
        private readonly AttributeExtractor extractor;
        private readonly ConcurrentDictionary<(INamedTypeSymbol, string), ValidationPropertyDescriptor> cache
            = new ConcurrentDictionary<(INamedTypeSymbol, string), ValidationPropertyDescriptor>();

        public ValidationPropertyDescriptorFactory(
            CSharpCompilation compilation,
            ValidationTypeDescriptorFactory descriptorFactory,
            AttributeExtractor extractor)
        {
            this.compilation = compilation;
            this.descriptorFactory = descriptorFactory;
            this.extractor = extractor;
        }

        public ValidationPropertyDescriptor Create(ITypeSymbol containingType, string name)
        {
            var property = (IPropertySymbol)containingType.GetMembers(name)
                .FirstOrDefault(s => s is IPropertySymbol);
            if (property == null)
            {
                return null;
            }

            return Create(property);
        }

        public ValidationPropertyDescriptor Create(IPropertySymbol propertySymbol)
        {
            var field = (IFieldSymbol)propertySymbol.ContainingType
                .GetMembers($"{propertySymbol.MetadataName}{ValidationPropertyDescriptor.PropertySuffix}")
                .FirstOrDefault(s => s is IFieldSymbol);
            if (field == null)
            {
                return CreateVirtual(propertySymbol);
            }

            return CreateRegular(propertySymbol, field);
        }

        public ValidationPropertyDescriptor CreateVirtual(IPropertySymbol propertySymbol)
        {
            return cache.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                var manipulationAttribute = extractor
                    .GetAttribute<DataContextStackManipulationAttribute>(propertySymbol);
                return new ValidationPropertyDescriptor(
                    propertySymbol: propertySymbol,
                    fieldSymbol: null,
                    declaringType: descriptorFactory.Create(propertySymbol.ContainingType),
                    propertyType: descriptorFactory.Create(propertySymbol.Type),
                    markupOptions: extractor.GetAttribute<MarkupOptionsAttribute>(propertySymbol),
                    changeAttributes: extractor.GetAttributes<DataContextChangeAttribute>(propertySymbol),
                    manipulationAttribute: manipulationAttribute);
            });
        }

        public ValidationPropertyDescriptor CreateRegular(IPropertySymbol propertySymbol, IFieldSymbol fieldSymbol)
        {
            return cache.GetOrAdd((propertySymbol.ContainingType, propertySymbol.MetadataName), _ =>
            {
                var manipulationAttribute = extractor
                    .GetAttribute<DataContextStackManipulationAttribute>(propertySymbol);
                var markupOptions = extractor.GetAttribute<MarkupOptionsAttribute>(propertySymbol)
                    ?? new MarkupOptionsAttribute
                    {
                        AllowBinding = true,
                        AllowHardCodedValue = true,
                        MappingMode = MappingMode.Attribute,
                        Name = propertySymbol.MetadataName
                    };
                return new ValidationPropertyDescriptor(
                    propertySymbol: propertySymbol,
                    fieldSymbol: fieldSymbol,
                    declaringType: descriptorFactory.Create(fieldSymbol.ContainingType),
                    propertyType: descriptorFactory.Create(fieldSymbol.Type),
                    markupOptions: markupOptions,
                    changeAttributes: extractor.GetAttributes<DataContextChangeAttribute>(propertySymbol),
                    manipulationAttribute: manipulationAttribute);
            });
        }

        public ImmutableArray<ValidationPropertyDescriptor> CreateMany(ITypeSymbol containingType)
        {
            return containingType.GetMembers()
                .OfType<IPropertySymbol>()
                .Select(Create)
                .ToImmutableArray();
        }
    }
}