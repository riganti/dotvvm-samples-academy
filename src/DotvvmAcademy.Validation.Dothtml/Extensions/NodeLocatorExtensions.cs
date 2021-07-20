﻿using DotVVM.Framework.Compilation.ControlTree;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal static class NodeLocatorExtensions
    {
        public static IEnumerable<TNode> Locate<TNode>(this NodeLocator locator, XPathExpression expression)
            where TNode : IAbstractTreeNode
        {
            return locator.Locate(expression)
                .OfType<TNode>();
        }

        public static TNode LocateSingle<TNode>(this NodeLocator locator, XPathExpression expression)
            where TNode : IAbstractTreeNode
        {
            return locator.Locate<TNode>(expression)
                .Single();
        }
    }
}
