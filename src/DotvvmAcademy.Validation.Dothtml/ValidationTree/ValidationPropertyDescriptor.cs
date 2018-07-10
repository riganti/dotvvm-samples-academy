using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationPropertyDescriptor : IPropertyDescriptor
    {
        public bool IsBindingProperty { get; }

        public string FullName { get; }

        public bool IsVirtual { get; }

        public string Name { get; }

        public MarkupOptionsAttribute MarkupOptions { get; }

        public DataContextChangeAttribute[] DataContextChangeAttributes { get; }

        public DataContextStackManipulationAttribute DataContextManipulationAttribute { get; }

        public ValidationTypeDescriptor DeclaringType { get; }

        public ValidationTypeDescriptor PropertyType { get; }

        ITypeDescriptor IControlAttributeDescriptor.DeclaringType => DeclaringType;

        ITypeDescriptor IControlAttributeDescriptor.PropertyType => PropertyType;
    }
}