using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationPropertyGroupDescriptor : IPropertyGroupDescriptor
    {
        public string[] Prefixes { get; }

        public ValidationTypeDescriptor CollectionType { get; }

        public ValidationTypeDescriptor DeclaringType { get; }

        public ValidationTypeDescriptor PropertyType { get; }

        public string Name { get; }

        public MarkupOptionsAttribute MarkupOptions { get; }

        public DataContextChangeAttribute[] DataContextChangeAttributes { get; }

        public DataContextStackManipulationAttribute DataContextManipulationAttribute { get; }

        ITypeDescriptor IControlAttributeDescriptor.PropertyType => PropertyType;

        ITypeDescriptor IPropertyGroupDescriptor.CollectionType => CollectionType;

        ITypeDescriptor IControlAttributeDescriptor.DeclaringType => DeclaringType;

        public ValidationPropertyDescriptor GetDotvvmProperty(string name)
        {
            throw new NotImplementedException();
        }

        IPropertyDescriptor IPropertyGroupDescriptor.GetDotvvmProperty(string name)
            => GetDotvvmProperty(name);
    }
}