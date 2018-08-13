using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationCollectionPropertyGroup : IPropertyGroupDescriptor
    {
        public ValidationCollectionPropertyGroup(
            IPropertySymbol propertySymbol,
            ValidationTypeDescriptor propertyType,
            ValidationTypeDescriptor collectionType,
            ValidationTypeDescriptor declaringType,
            MarkupOptionsAttribute markupOptions,
            ImmutableArray<DataContextChangeAttribute> dataContextChangeAttributes,
            DataContextStackManipulationAttribute dataContextManipulationAttribute,
            PropertyGroupAttribute propertyGroupAttribute)
        {
            PropertySymbol = propertySymbol;
            PropertyType = propertyType;
            CollectionType = collectionType;
            DeclaringType = declaringType;
            MarkupOptions = markupOptions;
            DataContextChangeAttributes = dataContextChangeAttributes;
            DataContextManipulationAttribute = dataContextManipulationAttribute;
            PropertyGroupAttribute = propertyGroupAttribute;

            Name = propertySymbol.Name;
        }

        public ValidationTypeDescriptor CollectionType { get; }

        public ImmutableArray<DataContextChangeAttribute> DataContextChangeAttributes { get; }

        public DataContextStackManipulationAttribute DataContextManipulationAttribute { get; }

        public ValidationTypeDescriptor DeclaringType { get; }

        public MarkupOptionsAttribute MarkupOptions { get; }

        public string Name { get; }

        public PropertyGroupAttribute PropertyGroupAttribute { get; }

        public IPropertySymbol PropertySymbol { get; }

        public ValidationTypeDescriptor PropertyType { get; }

        ITypeDescriptor IPropertyGroupDescriptor.CollectionType => CollectionType;

        public string[] Prefixes => PropertyGroupAttribute.Prefixes;

        DataContextChangeAttribute[] IControlAttributeDescriptor.DataContextChangeAttributes
        {
            get
            {
                if (DataContextChangeAttributes.IsDefaultOrEmpty)
                {
                    return Array.Empty<DataContextChangeAttribute>();
                }

                return DataContextChangeAttributes.ToArray();
            }
        }

        ITypeDescriptor IControlAttributeDescriptor.DeclaringType => DeclaringType;


        ITypeDescriptor IControlAttributeDescriptor.PropertyType => PropertyType;

        IPropertyDescriptor IPropertyGroupDescriptor.GetDotvvmProperty(string name)
        {
            throw new NotSupportedException($"Use {nameof(ValidationPropertyFactory)} instead.");
        }
    }
}