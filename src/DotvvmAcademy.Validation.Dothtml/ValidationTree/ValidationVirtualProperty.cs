using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationVirtualProperty : IPropertyDescriptor
    {
        public ValidationVirtualProperty(
            IPropertySymbol propertySymbol,
            ValidationTypeDescriptor propertyType,
            ValidationTypeDescriptor declaringType,
            MarkupOptionsAttribute markupOptions,
            ImmutableArray<DataContextChangeAttribute> dataContextChangeAttributes,
            DataContextStackManipulationAttribute dataContextManipulationAttribute)
        {
            PropertySymbol = propertySymbol;
            PropertyType = propertyType;
            DeclaringType = declaringType;
            MarkupOptions = markupOptions;
            DataContextChangeAttributes = dataContextChangeAttributes;
            DataContextManipulationAttribute = dataContextManipulationAttribute;

            Name = propertySymbol.Name;
            FullName = $"{declaringType}.{Name}";
            IsBindingProperty = PropertyType.IsAssignableTo(typeof(IBinding));
        }

        public ImmutableArray<DataContextChangeAttribute> DataContextChangeAttributes { get; }

        public DataContextStackManipulationAttribute DataContextManipulationAttribute { get; }

        public ValidationTypeDescriptor DeclaringType { get; }

        public string FullName { get; }

        public bool IsBindingProperty { get; }

        public bool IsVirtual { get; } = true;

        public MarkupOptionsAttribute MarkupOptions { get; }

        public string Name { get; }

        public IPropertySymbol PropertySymbol { get; }

        public ValidationTypeDescriptor PropertyType { get; }

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
    }
}