using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("Directive: {Name,nq} {Value,nq}")]
    public class ValidationDirective : ValidationTreeNode, IAbstractDirective
    {
        public ValidationDirective(DothtmlDirectiveNode node) : base(node)
        {
            Name = node.Name;
            Value = node.Value;
        }

        public string Name { get; }

        public string Value { get; }
    }
}