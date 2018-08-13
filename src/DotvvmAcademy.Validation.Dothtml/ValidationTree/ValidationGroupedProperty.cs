using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using System;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    public class ValidationGroupedProperty : IPropertyDescriptor
    {
        public ValidationGroupedProperty(
            IPropertyGroupDescriptor propertyGroup,
            string groupMemberName)
        {
            PropertyGroup = propertyGroup;
            GroupMemberName = groupMemberName;

            Name = $"{PropertyGroup.Name}-{GroupMemberName}";
            FullName = $"{PropertyGroup.DeclaringType}";
            IsBindingProperty = (((ValidationTypeDescriptor)PropertyGroup.PropertyType)?.IsAssignableTo(typeof(IBinding)))
                .GetValueOrDefault();
            MarkupOptions = new MarkupOptionsAttribute()
            {
                AllowBinding = true,
                AllowHardCodedValue = true,
                MappingMode = MappingMode.Attribute,
                Name = Name
            };
        }

        public DataContextStackManipulationAttribute DataContextManipulationAttribute { get; }

        public ValidationTypeDescriptor DeclaringType => (ValidationTypeDescriptor)PropertyGroup.DeclaringType;

        public string FullName { get; }

        public string GroupMemberName { get; }

        public bool IsBindingProperty { get; }

        public bool IsVirtual { get; } = true;

        public MarkupOptionsAttribute MarkupOptions { get; }

        public string Name { get; }

        public IPropertyGroupDescriptor PropertyGroup { get; }

        public ValidationTypeDescriptor PropertyType => (ValidationTypeDescriptor)PropertyGroup.PropertyType;

        DataContextChangeAttribute[] IControlAttributeDescriptor.DataContextChangeAttributes { get; }
            = Array.Empty<DataContextChangeAttribute>();

        ITypeDescriptor IControlAttributeDescriptor.DeclaringType => DeclaringType;

        ITypeDescriptor IControlAttributeDescriptor.PropertyType => PropertyType;
    }
}