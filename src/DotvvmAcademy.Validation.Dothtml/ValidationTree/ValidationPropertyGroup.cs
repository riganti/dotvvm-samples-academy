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
    public class ValidationPropertyGroup : IPropertyGroupDescriptor
    {
        public ValidationPropertyGroup(
            IPropertySymbol propertySymbol,
            IFieldSymbol fieldSymbol,
            ValidationTypeDescriptor propertyType,
            ValidationTypeDescriptor declaringType,
            MarkupOptionsAttribute markupOptions,
            ImmutableArray<DataContextChangeAttribute> dataContextChangeAttributes,
            DataContextStackManipulationAttribute dataContextManipulationAttribute,
            ImmutableArray<string> prefixes)
        {
            PropertySymbol = propertySymbol;
            FieldSymbol = fieldSymbol;
            PropertyType = propertyType;
            DeclaringType = declaringType;
            MarkupOptions = markupOptions;
            DataContextChangeAttributes = dataContextChangeAttributes;
            DataContextManipulationAttribute = dataContextManipulationAttribute;
            Prefixes = prefixes;

            Name = propertySymbol.Name;
        }

        public ImmutableArray<DataContextChangeAttribute> DataContextChangeAttributes { get; }

        public DataContextStackManipulationAttribute DataContextManipulationAttribute { get; }

        public ValidationTypeDescriptor DeclaringType { get; }

        public IFieldSymbol FieldSymbol { get; }

        public MarkupOptionsAttribute MarkupOptions { get; }

        public string Name { get; }

        public ImmutableArray<string> Prefixes { get; }

        public IPropertySymbol PropertySymbol { get; }

        public ValidationTypeDescriptor PropertyType { get; }

        ITypeDescriptor IPropertyGroupDescriptor.CollectionType { get; }

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

        string[] IPropertyGroupDescriptor.Prefixes => Prefixes.ToArray();

        ITypeDescriptor IControlAttributeDescriptor.PropertyType => PropertyType;

        IPropertyDescriptor IPropertyGroupDescriptor.GetDotvvmProperty(string name)
        {
            throw new NotSupportedException($"Use {nameof(ValidationPropertyFactory)} instead.");
        }
    }
}