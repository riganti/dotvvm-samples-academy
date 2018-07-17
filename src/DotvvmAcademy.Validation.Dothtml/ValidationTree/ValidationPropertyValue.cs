using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("PropertyValue: {Property.FullName,nq}, Value = {Value,nq}")]
    internal class ValidationPropertyValue : ValidationPropertySetter, IAbstractPropertyValue
    {
        public ValidationPropertyValue(
            DothtmlNode node,
            ValidationPropertyDescriptor property,
            object value)
            : base(node, property)
        {
            Value = value;
        }

        public object Value { get; }
    }
}