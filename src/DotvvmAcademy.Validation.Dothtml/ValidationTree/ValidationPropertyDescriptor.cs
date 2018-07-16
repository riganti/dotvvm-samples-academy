using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationPropertyDescriptor : IPropertyDescriptor
    {
        public const string PropertySuffix = "Property";

        public ValidationPropertyDescriptor(
            IPropertySymbol propertySymbol,
            IFieldSymbol fieldSymbol,
            ValidationTypeDescriptor declaringType,
            ValidationTypeDescriptor propertyType,
            MarkupOptionsAttribute markupOptions,
            ImmutableArray<DataContextChangeAttribute> changeAttributes,
            DataContextStackManipulationAttribute manipulationAttribute)
        {
            PropertySymbol = propertySymbol;
            FieldSymbol = fieldSymbol;
            DeclaringType = declaringType;
            PropertyType = propertyType;
            MarkupOptions = markupOptions;
            DataContextManipulationAttribute = manipulationAttribute;

            Name = SanitizeName(propertySymbol.MetadataName);
            FullName = $"{DeclaringType.FullName}.{Name}";
            IsVirtual = fieldSymbol == null;
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

        public IPropertySymbol PropertySymbol { get; }

        public ValidationTypeDescriptor PropertyType { get; }

        DataContextChangeAttribute[] IControlAttributeDescriptor.DataContextChangeAttributes
            => DataContextChangeAttributes.ToArray();

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