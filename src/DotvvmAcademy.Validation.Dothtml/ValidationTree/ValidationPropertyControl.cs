using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("PropertyControl: {Property.FullName,nq}, Control = {Control.Metadata.Type.FullName,nq}")]
    public class ValidationPropertyControl : ValidationPropertySetter, IAbstractPropertyControl
    {
        public ValidationPropertyControl(
            DothtmlNode node,
            IPropertyDescriptor property,
            ValidationControl control)
            : base(node, property)
        {
            Control = control;
            Control.Parent = this;
        }

        public ValidationControl Control { get; }

        IAbstractControl IAbstractPropertyControl.Control => Control;
    }
}