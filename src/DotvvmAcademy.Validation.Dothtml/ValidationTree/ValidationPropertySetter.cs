using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationPropertySetter : ValidationTreeNode, IAbstractPropertySetter
    {
        public ValidationPropertySetter(DothtmlNode node, DotvvmProperty property) : base(node)
        {
            Property = property;
        }

        public DotvvmProperty Property { get; }

        IPropertyDescriptor IAbstractPropertySetter.Property => Property;
    }
}