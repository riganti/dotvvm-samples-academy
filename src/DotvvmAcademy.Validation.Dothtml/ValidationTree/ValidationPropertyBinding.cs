using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("PropertyBinding: {Property.FullName,nq}, BindingType = {Binding.BindingType.Name,nq}")]
    public class ValidationPropertyBinding : ValidationPropertySetter, IAbstractPropertyBinding
    {
        public ValidationPropertyBinding(
            DothtmlNode node,
            IPropertyDescriptor property,
            ValidationBinding binding)
            : base(node, property)
        {
            Binding = binding;
            Binding.Parent = this;
        }

        public ValidationBinding Binding { get; }

        IAbstractBinding IAbstractPropertyBinding.Binding => Binding;
    }
}