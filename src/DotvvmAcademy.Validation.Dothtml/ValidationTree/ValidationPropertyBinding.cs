using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationPropertyBinding : ValidationPropertySetter, IAbstractPropertyBinding
    {
        public ValidationPropertyBinding(
            DothtmlNode node,
            DotvvmProperty property,
            ValidationBinding binding)
            : base(node, property)
        {
            Binding = binding;
        }

        public ValidationBinding Binding { get; }

        IAbstractBinding IAbstractPropertyBinding.Binding => Binding;
    }
}