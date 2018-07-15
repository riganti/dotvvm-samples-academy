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
            var controlType = typeFactory.Create(symbol);

            return cache.GetOrAdd(symbol, s => new ValidationControlMetadata(
                controlType: controlType,
                changeAttributes: ExtractChangeAttributes(symbol),
                manipulationAttribute: ExtractManipulationAttribute(symbol),
                markupOptionsAttribute: ExtractMarkupOptionsAttribute(symbol),
                properties: ,
                propertyGroupMatchers: );
        }

        private DataContextStackManipulationAttribute ExtractManipulationAttribute(ISymbol symbol)
        {
            var data = symbol.GetAttributes().FirstOrDefault(a => IsManipulationAttribute(a.AttributeClass));
            if (data?.AttributeClass.Equals(attributePopManipulation) == true)
            {
                return new PopDataContextManipulationAttribute();
            }
            return null;
        }

        private ImmutableArray<DataContextChangeAttribute> ExtractChangeAttributes(ISymbol symbol)
        {
            var builder = ImmutableArray.CreateBuilder<DataContextChangeAttribute>();
            var roslynAttributes = symbol.GetAttributes().Where(a => IsChangeAttribute(a.AttributeClass));
            foreach (var attribute in roslynAttributes)
            {
                if (attribute.AttributeClass.Equals(attributeCollectionElementChange))
                {
                    if (attribute.ConstructorArguments.Length != 1)
                    {
                        continue;
                    }
                    var order = (attribute.ConstructorArguments[0].Value as int?).GetValueOrDefault();
                    builder.Add(new CollectionElementDataContextChangeAttribute(order));
                }
                else if (attribute.AttributeClass.Equals(attributeConstantChange))
                {
                    if (attribute.ConstructorArguments.Length == 0 || attribute.ConstructorArguments.Length > 2)
                    {
                        continue;
                    }
                    var type = attribute.ConstructorArguments[0].Value as Type;
                    var order = attribute.ConstructorArguments.Length == 2
                        ? (attribute.ConstructorArguments[1].Value as int?).GetValueOrDefault()
                        : 0;
                    builder.Add(new ConstantDataContextChangeAttribute(type, order));
                }
                else if (attribute.AttributeClass.Equals(attributeControlPropertyBindingChange))
                {
                    if (attribute.ConstructorArguments.Length == 0 || attribute.ConstructorArguments.Length > 2)
                    {
                        continue;
                    }
                    var propertyName = attribute.ConstructorArguments[0].Value as string;
                    var order = attribute.ConstructorArguments.Length == 2
                        ? (attribute.ConstructorArguments[1].Value as int?).GetValueOrDefault()
                        : 0;
                    builder.Add(new ControlPropertyBindingDataContextChangeAttribute(propertyName, order));
                }
                else if (attribute.AttributeClass.Equals(attributeControlPropertyTypeChange))
                {
                    if (attribute.ConstructorArguments.Length == 0 || attribute.ConstructorArguments.Length > 2)
                    {
                        continue;
                    }
                    var propertyName = attribute.ConstructorArguments[0].Value as string;
                    var order = attribute.ConstructorArguments.Length == 2
                        ? (attribute.ConstructorArguments[1].Value as int?).GetValueOrDefault()
                        : 0;
                    builder.Add(new ControlPropertyTypeDataContextChangeAttribute(propertyName, order));
                }
                else if (attribute.AttributeClass.Equals(attributeGenericTypeChange))
                {
                    if (attribute.ConstructorArguments.Length == 0 || attribute.ConstructorArguments.Length > 2)
                    {
                        continue;
                    }
                    var type = attribute.ConstructorArguments[0].Value as Type;
                    var order = attribute.ConstructorArguments.Length == 2
                        ? (attribute.ConstructorArguments[1].Value as int?).GetValueOrDefault()
                        : 0;
                    builder.Add(new GenericTypeDataContextChangeAttribute(type, order));
                }
            }
            return builder.ToImmutable();
        }

        private ControlMarkupOptionsAttribute ExtractMarkupOptionsAttribute(ISymbol symbol)
        {
            var data = symbol.GetAttributes().FirstOrDefault(a => a.AttributeClass.Equals(attributeMarkupOptions));
            if (data == null)
            {
                return null;
            }
            var allowContent = (data.NamedArguments
                .FirstOrDefault(a => a.Key == nameof(ControlMarkupOptionsAttribute.AllowContent))
                .Value.Value as bool?)
                .GetValueOrDefault();
            var defaultContentProperty = data.NamedArguments
                .FirstOrDefault(a => a.Key == nameof(ControlMarkupOptionsAttribute.DefaultContentProperty))
                .Value.Value as string;
            return new ControlMarkupOptionsAttribute
            {
                AllowContent = allowContent,
                DefaultContentProperty = defaultContentProperty
            };
        }

        private bool IsChangeAttribute(ITypeSymbol symbol)
            => compilation.ClassifyConversion(symbol, attributeChange).Exists;

        private bool IsManipulationAttribute(ITypeSymbol symbol)
            => compilation.ClassifyConversion(symbol, attributeManipulation).Exists;

        private ITypeSymbol GetAttributeSymbol<TAttribute>()
            => compilation.GetTypeByMetadataName(typeof(TAttribute).FullName);
    }
}