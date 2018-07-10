﻿using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal abstract class ValidationTreeNode : IAbstractTreeNode
    {
        public ValidationTreeNode(DothtmlNode node)
        {
            DothtmlNode = node;
        }

        public DothtmlNode DothtmlNode { get; }

        public ValidationTreeNode Parent { get; protected set; }

        IAbstractTreeNode IAbstractTreeNode.Parent => Parent;

        public ValidationTreeRoot TreeRoot { get; protected set; }

        IAbstractTreeRoot IAbstractTreeNode.TreeRoot => TreeRoot;
    }
}