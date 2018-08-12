using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("PropertySetter: {Property.FullName,nq}")]
    public abstract class ValidationPropertySetter : ValidationTreeNode, IAbstractPropertySetter
    {
        public ValidationPropertySetter(DothtmlNode node, IPropertyDescriptor property) : base(node)
        {
            Property = property;
        }

        public IPropertyDescriptor Property { get; }

        IPropertyDescriptor IAbstractPropertySetter.Property => Property;
    }
}