using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("PropertySetter: {Property.FullName,nq}")]
    public abstract class ValidationPropertySetter : ValidationTreeNode, IAbstractPropertySetter
    {
        public ValidationPropertySetter(DothtmlNode node, ValidationPropertyDescriptor property) : base(node)
        {
            Property = property;
        }

        public ValidationPropertyDescriptor Property { get; }

        IPropertyDescriptor IAbstractPropertySetter.Property => Property;
    }
}