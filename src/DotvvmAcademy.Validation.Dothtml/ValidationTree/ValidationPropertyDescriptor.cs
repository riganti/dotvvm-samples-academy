using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("PropertyDescriptor: {FullName,nq}")]
    public class ValidationPropertyDescriptor : IPropertyDescriptor
    {
        public const string GroupSeparator = ":";
        public const string PropertySuffix = "Property";

        /// <summary>
        /// Creates a new attached property.
        /// </summary>
        public ValidationPropertyDescriptor(
            IFieldSymbol fieldSymbol,
            ValidationTypeDescriptor declaringType,
            ValidationTypeDescriptor propertyType,
            MarkupOptionsAttribute markupOptions,
            ImmutableArray<DataContextChangeAttribute> changeAttributes,
            DataContextStackManipulationAttribute manipulationAttribute)
        {
            FieldSymbol = fieldSymbol;
            DeclaringType = declaringType;
            PropertyType = propertyType;
            MarkupOptions = markupOptions;
            DataContextChangeAttributes = changeAttributes;
            DataContextManipulationAttribute = manipulationAttribute;

            Name = SanitizeName(FieldSymbol.MetadataName);
            FullName = $"{DeclaringType.FullName}.{Name}";
            IsAttached = true;
            IsBindingProperty = PropertyType.IsAssignableTo(typeof(IBinding));
        }

        /// <summary>
        /// Creates a new virtual property.
        /// </summary>
        public ValidationPropertyDescriptor(
            IPropertySymbol propertySymbol,
            ValidationTypeDescriptor declaringType,
            ValidationTypeDescriptor propertyType,
            MarkupOptionsAttribute markupOptions,
            ImmutableArray<DataContextChangeAttribute> changeAttributes,
            DataContextStackManipulationAttribute manipulationAttribute)
        {
            PropertySymbol = propertySymbol;
            DeclaringType = declaringType;
            PropertyType = propertyType;
            MarkupOptions = markupOptions;
            DataContextManipulationAttribute = manipulationAttribute;

            Name = PropertySymbol.MetadataName;
            FullName = $"{DeclaringType.FullName}.{Name}";
            IsVirtual = true;
            IsBindingProperty = PropertyType.IsAssignableTo(typeof(IBinding));
        }

        /// <summary>
        /// Creates a new regular property.
        /// </summary>
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

            Name = PropertySymbol.MetadataName;
            FullName = $"{DeclaringType.FullName}.{Name}";
            IsBindingProperty = PropertyType.IsAssignableTo(typeof(IBinding));
        }

        /// <summary>
        /// Creates a new grouped property.
        /// </summary>
        public ValidationPropertyDescriptor(
            ValidationPropertyGroupDescriptor propertyGroup,
            string groupMemberName)
        {
            PropertyGroup = propertyGroup;
            GroupMemberName = groupMemberName;

            PropertySymbol = PropertyGroup.PropertySymbol;
            DeclaringType = PropertyGroup.DeclaringType;
            PropertyType = PropertyGroup.PropertyType;
            Name = $"{propertyGroup.Name}{GroupSeparator}{groupMemberName}";
            FullName = $"{DeclaringType.FullName}.{Name}";
            IsGrouped = true;
            IsBindingProperty = PropertyType.IsAssignableTo(typeof(IBinding));
        }

        public bool IsAttached { get; }

        public ImmutableArray<DataContextChangeAttribute> DataContextChangeAttributes { get; }

        public DataContextStackManipulationAttribute DataContextManipulationAttribute { get; }

        public ValidationTypeDescriptor DeclaringType { get; }

        public IFieldSymbol FieldSymbol { get; }

        public string FullName { get; }

        public string GroupMemberName { get; }

        public bool IsBindingProperty { get; }

        public bool IsGrouped { get; }

        public bool IsVirtual { get; }

        public MarkupOptionsAttribute MarkupOptions { get; }

        public string Name { get; }

        public ValidationPropertyGroupDescriptor PropertyGroup { get; }

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