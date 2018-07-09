using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationPropertyValue : ValidationPropertySetter, IAbstractPropertyValue
    {
        public ValidationPropertyValue(
            DothtmlNode node,
            DotvvmProperty property,
            object value)
            : base(node, property)
        {
            Value = value;
        }

        public object Value { get; }
    }
}