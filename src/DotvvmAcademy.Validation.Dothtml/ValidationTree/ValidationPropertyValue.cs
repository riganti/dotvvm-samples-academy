using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("PropertyValue: {Property.FullName,nq}, Value = {Value,nq}")]
    public class ValidationPropertyValue : ValidationPropertySetter, IAbstractPropertyValue
    {
        public ValidationPropertyValue(
            DothtmlNode node,
            IPropertyDescriptor property,
            object value)
            : base(node, property)
        {
            Value = value;
        }

        public object Value { get; }
    }
}