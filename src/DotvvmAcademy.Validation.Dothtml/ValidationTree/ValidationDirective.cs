﻿using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationDirective : ValidationTreeNode, IAbstractDirective
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