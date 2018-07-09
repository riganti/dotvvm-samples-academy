using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationPropertyControl : ValidationPropertySetter, IAbstractPropertyControl
    {
        public ValidationPropertyControl(
            DothtmlNode node,
            DotvvmProperty property,
            ValidationControl control)
            : base(node, property)
        {
            Control = control;
        }

        public ValidationControl Control { get; }

        IAbstractControl IAbstractPropertyControl.Control => Control;
    }
}