using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControlMetadataFactory
    {
        private readonly CSharpCompilation compilation;
        private readonly ValidationTypeDescriptorFactory descriptorFactory;
        private readonly ValidationControlTypeFactory typeFactory;
        private readonly ConcurrentDictionary<ITypeSymbol, ValidationControlMetadata> cache
            = new ConcurrentDictionary<ITypeSymbol, ValidationControlMetadata>();
        private readonly ITypeSymbol attributeMarkupOptions;
        private readonly ITypeSymbol attributeManipulation;
        private readonly ITypeSymbol attributeChange;
        private readonly ITypeSymbol attributePopManipulation;
        private readonly ITypeSymbol attributeCollectionElementChange;
        private readonly ITypeSymbol attributeConstantChange;
        private readonly ITypeSymbol attributeControlPropertyBindingChange;
        private readonly ITypeSymbol attributeControlPropertyTypeChange;
        private readonly ITypeSymbol attributeGenericTypeChange;

        public ValidationControlMetadataFactory(
            CSharpCompilation compilation,
            ValidationTypeDescriptorFactory descriptorFactory,
            ValidationControlTypeFactory typeFactory)
        {
            this.compilation = compilation;
            this.descriptorFactory = descriptorFactory;
            this.typeFactory = typeFactory;

            attributeMarkupOptions = GetAttributeSymbol<ControlMarkupOptionsAttribute>();
            attributeManipulation = GetAttributeSymbol<DataContextStackManipulationAttribute>();
            attributeChange = GetAttributeSymbol<DataContextChangeAttribute>();
            attributePopManipulation = GetAttributeSymbol<PopDataContextManipulationAttribute>();
            attributeCollectionElementChange = GetAttributeSymbol<CollectionElementDataContextChangeAttribute>();
            attributeConstantChange = GetAttributeSymbol<ConstantDataContextChangeAttribute>();
            attributeControlPropertyBindingChange = GetAttributeSymbol<ControlPropertyBindingDataContextChangeAttribute>();
            attributeControlPropertyTypeChange = GetAttributeSymbol<ControlPropertyTypeDataContextChangeAttribute>();
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
            return cache.GetOrAdd(symbol, s => new ValidationControlMetadata());
        }

        private DataContextStackManipulationAttribute ExtractManipulationAttribute(ITypeSymbol symbol)
        {
            var data = symbol.GetAttributes().FirstOrDefault(a => IsManipulationAttribute(a.AttributeClass));
            if (data.AttributeClass.Equals(attributePopManipulation))
            {
                return new PopDataContextManipulationAttribute();
            }
            return null;
        }

        private ImmutableArray<DataContextChangeAttribute> ExtractChangeAttributes(ITypeSymbol symbol)
        {
            var builder = ImmutableArray.CreateBuilder<DataContextChangeAttribute>();
            var roslynAttributes = symbol.GetAttributes().Where(a => IsChangeAttribute(a.AttributeClass));

            foreach(var attribute in roslynAttributes)
            {
                if (attribute.AttributeClass.Equals(attributeCollectionElementChange))
                {
                    var order = attribute.ConstructorArguments.FirstOrDefault().Value as int? ?? 0;
                    builder.Add(new CollectionElementDataContextChangeAttribute(order));
                }
            }
        }

        private ControlMarkupOptionsAttribute ExtractMarkupOptionsAttribute()
        {

        }

        private bool IsChangeAttribute(ITypeSymbol symbol)
            => compilation.ClassifyConversion(symbol, attributeChange).Exists;

        private bool IsManipulationAttribute(ITypeSymbol symbol)
            => compilation.ClassifyConversion(symbol, attributeManipulation).Exists;

        private ITypeSymbol GetAttributeSymbol<TAttribute>()
            => compilation.GetTypeByMetadataName(typeof(TAttribute).FullName);
    }
}