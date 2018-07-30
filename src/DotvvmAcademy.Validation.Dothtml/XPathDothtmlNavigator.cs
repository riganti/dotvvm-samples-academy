using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    [DebuggerDisplay("{ToString()}")]
    internal class XPathDothtmlNavigator : XPathNavigator
    {
        public XPathDothtmlNavigator(NameTable nameTable, XPathDothtmlNode current)
        {
            NameTable = nameTable;
            Node = current;
        }

        private XPathDothtmlNavigator(XPathDothtmlNavigator other)
        {
            Node = other.Node;
        }

        public override string BaseURI { get; } = string.Empty;

        public override bool CanEdit { get; } = false;

        public XPathDothtmlNode Node { get; private set; }

        public override bool HasAttributes => !Node.Attributes.IsDefaultOrEmpty;

        public override bool HasChildren => !Node.Children.IsDefaultOrEmpty;

        public override bool IsEmptyElement { get; } = false;

        public override string LocalName => Node.LocalName ?? string.Empty;

        public override string Name => string.Empty;

        public override string NamespaceURI => Node.Namespace ?? string.Empty;

        public override XmlNameTable NameTable { get; }

        public override XPathNodeType NodeType => Node.NodeType;

        public override string Prefix => Node.Prefix ?? string.Empty;

        public override object TypedValue => Node.Value;

        public override object UnderlyingObject => Node.UnderlyingObject;

        public override string Value => Node.Value.ToString();

        public override XPathNavigator Clone()
        {
            return new XPathDothtmlNavigator(this);
        }

        public override bool IsSamePosition(XPathNavigator other)
        {
            if (!(other is XPathDothtmlNavigator resolvedOther))
            {
                return false;
            }
            return ReferenceEquals(Node, resolvedOther.Node);
        }

        public override bool MoveTo(XPathNavigator other)
        {
            if (!(other is XPathDothtmlNavigator resolvedOther))
            {
                return false;
            }
            Node = resolvedOther.Node;
            return true;
        }

        public override bool MoveToFirst()
        {
            if (Node.NodeType != XPathNodeType.Element || Node.FirstSibling == null)
            {
                return false;
            }
            Node = Node.FirstSibling;
            return true;
        }

        public override bool MoveToFirstAttribute()
        {
            if (Node.Attributes.IsDefaultOrEmpty)
            {
                return false;
            }
            Node = Node.Attributes[0];
            return true;
        }

        public override bool MoveToFirstChild()
        {
            if (Node.Children.IsDefaultOrEmpty)
            {
                return false;
            }
            Node = Node.Children[0];
            return true;
        }

        public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope) => false;

        public override bool MoveToId(string id) => false;

        public override bool MoveToNext()
        {
            if (Node.NextSibling == null || Node.NodeType == XPathNodeType.Attribute)
            {
                return false;
            }
            Node = Node.NextSibling;
            return true;
        }

        public override bool MoveToNextAttribute()
        {
            if (Node.NextSibling == null || Node.NodeType != XPathNodeType.Attribute)
            {
                return false;
            }
            Node = Node.NextSibling;
            return true;
        }

        public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope) => false;

        public override bool MoveToParent()
        {
            if (Node.Parent != null)
            {
                Node = Node.Parent;
                return true;
            }
            return false;
        }

        public override bool MoveToPrevious()
        {
            if (Node.PreviousSibling == null || Node.NodeType == XPathNodeType.Attribute)
            {
                return false;
            }
            Node = Node.PreviousSibling;
            return true;
        }

        public override void MoveToRoot()
        {
            Node = Node.Root;
        }

        public override string ToString()
        {
            return Node.ToString();
        }
    }
}