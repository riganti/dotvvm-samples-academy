using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    [DebuggerDisplay("{ToString()}")]
    internal class XPathDothtmlNavigator : XPathNavigator
    {
        public XPathDothtmlNavigator(XPathDothtmlNode current)
        {
            Current = current;
        }

        private XPathDothtmlNavigator(XPathDothtmlNavigator other)
        {
            Current = other.Current;
        }

        public override string BaseURI { get; } = "";

        public override bool CanEdit => false;

        public XPathDothtmlNode Current { get; private set; }

        public override bool HasAttributes => !Current.Attributes.IsDefaultOrEmpty;

        public override bool HasChildren => !Current.Children.IsDefaultOrEmpty;

        public override bool IsEmptyElement { get; } = false;

        public override string LocalName => Current.LocalName;

        public override string Name => throw new NotSupportedException();

        public override string NamespaceURI { get; } = "";

        public override XmlNameTable NameTable => Current.Root.NameTable;

        public override XPathNodeType NodeType => Current.NodeType;

        public override string Prefix => Current.Prefix;

        public override object TypedValue => Current.Value;

        public override object UnderlyingObject => Current.UnderlyingObject;

        public override string Value => TypedValue.ToString();

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
            return ReferenceEquals(Current, resolvedOther.Current);
        }

        public override bool MoveTo(XPathNavigator other)
        {
            if (!(other is XPathDothtmlNavigator resolvedOther))
            {
                return false;
            }
            Current = resolvedOther.Current;
            return true;
        }

        public override bool MoveToFirst()
        {
            if (Current.NodeType != XPathNodeType.Element || Current.FirstSibling == null)
            {
                return false;
            }
            Current = Current.FirstSibling;
            return true;
        }

        public override bool MoveToFirstAttribute()
        {
            if (Current.Attributes.IsDefaultOrEmpty)
            {
                return false;
            }
            Current = Current.Attributes[0];
            return true;
        }

        public override bool MoveToFirstChild()
        {
            if (Current.Children.IsDefaultOrEmpty)
            {
                return false;
            }
            Current = Current.Children[0];
            return true;
        }

        public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope) => false;

        public override bool MoveToId(string id) => false;

        public override bool MoveToNext()
        {
            if (Current.NextSibling == null || Current.NodeType == XPathNodeType.Attribute)
            {
                return false;
            }
            Current = Current.NextSibling;
            return true;
        }

        public override bool MoveToNextAttribute()
        {
            if (Current.NextSibling == null || Current.NodeType != XPathNodeType.Attribute)
            {
                return false;
            }
            Current = Current.NextSibling;
            return true;
        }

        public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope) => false;

        public override bool MoveToParent()
        {
            if (Current.Parent != null)
            {
                Current = Current.Parent;
                return true;
            }
            return false;
        }

        public override bool MoveToPrevious()
        {
            if (Current.PreviousSibling == null || Current.NodeType == XPathNodeType.Attribute)
            {
                return false;
            }
            Current = Current.PreviousSibling;
            return true;
        }

        public override void MoveToRoot()
        {
            Current = Current.Root;
        }

        public override string ToString()
        {
            return Current.ToString();
        }
    }
}