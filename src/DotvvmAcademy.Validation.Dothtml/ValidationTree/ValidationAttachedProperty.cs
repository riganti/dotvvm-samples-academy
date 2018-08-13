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
    public class ValidationAttachedProperty : IPropertyDescriptor
    {
        public const string PropertySuffix = "Property";

        public ValidationAttachedProperty(
            IFieldSymbol fieldSymbol,
            ValidationTypeDescriptor propertyType,
            ValidationTypeDescriptor declaringType,
            MarkupOptionsAttribute markupOptions,
            ImmutableArray<DataContextChangeAttribute> dataContextChangeAttributes,
            DataContextStackManipulationAttribute dataContextManipulationAttribute)
        {
            FieldSymbol = fieldSymbol;
            PropertyType = propertyType;
            DeclaringType = declaringType;
            MarkupOptions = markupOptions;
            DataContextChangeAttributes = dataContextChangeAttributes;
            DataContextManipulationAttribute = dataContextManipulationAttribute;

            Name = SanitizeName(fieldSymbol.Name);
            FullName = $"{declaringType}.{Name}";
            IsBindingProperty = PropertyType.IsAssignableTo(typeof(IBinding));
        }

        public ImmutableArray<DataContextChangeAttribute> DataContextChangeAttributes { get; }

        public DataContextStackManipulationAttribute DataContextManipulationAttribute { get; }

        public ValidationTypeDescriptor DeclaringType { get; }

        public IFieldSymbol FieldSymbol { get; }

        public string FullName { get; }

        public bool IsBindingProperty { get; }

        public bool IsVirtual { get; }

        public MarkupOptionsAttribute MarkupOptions { get; }

        public string Name { get; }

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

        public static string SanitizeName(string name)
        {
            if (name.EndsWith(PropertySuffix))
            {
                return name.Substring(0, name.Length - PropertySuffix.Length);
            }

            return name;
        }
    }
}