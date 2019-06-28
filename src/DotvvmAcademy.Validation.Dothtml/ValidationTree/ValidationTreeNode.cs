using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("TreeNode")]
    public abstract class ValidationTreeNode : IAbstractTreeNode
    {
        public ValidationTreeNode(DothtmlNode node)
        {
            DothtmlNode = node;
        }

        public DothtmlNode DothtmlNode { get; }

        public ValidationTreeNode Parent { get; set; }

        IAbstractTreeNode IAbstractTreeNode.Parent => Parent;

        public ValidationTreeRoot TreeRoot => GetTreeRoot();

        protected virtual ValidationTreeRoot GetTreeRoot()
        {
            return Parent.TreeRoot;
        }

        IAbstractTreeRoot IAbstractTreeNode.TreeRoot => TreeRoot;

    }
}