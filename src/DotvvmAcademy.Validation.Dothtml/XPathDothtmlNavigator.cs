using System;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathDothtmlNavigator : XPathNavigator
    {
        private XPathDothtmlNode current;

        public XPathDothtmlNavigator(XPathDothtmlElement current)
        {
            this.current = current;
        }

        private XPathDothtmlNavigator(XPathDothtmlNavigator other)
        {
            current = other.current;
        }

        public override string BaseURI { get; } = "";

        public override bool CanEdit => false;

        public override bool HasAttributes => current is XPathDothtmlElement element && !element.Attributes.IsDefaultOrEmpty;

        public override bool HasChildren => current is XPathDothtmlElement element && !element.Children.IsDefaultOrEmpty;

        public override bool IsEmptyElement { get; } = false;

        public override string LocalName => current.LocalName;

        public override string Name => throw new NotSupportedException();

        public override string NamespaceURI => throw new NotSupportedException();

        public override XmlNameTable NameTable => current.Root.NameTable;

        public override XPathNodeType NodeType => current.NodeType;

        public override string Prefix => current.Prefix;

        public override object TypedValue => throw new NotSupportedException();

        public override object UnderlyingObject => current.UnderlyingObject;

        public override string Value => throw new NotSupportedException();

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
            return ReferenceEquals(current, resolvedOther.current);
        }

        public override bool MoveTo(XPathNavigator other)
        {
            if (!(other is XPathDothtmlNavigator resolvedOther))
            {
                return false;
            }
            current = resolvedOther.current;
            return true;
        }

        public override bool MoveToFirst()
        {
            if (!(current is XPathDothtmlElement element) || element.FirstSibling == null)
            {
                return false;
            }
            current = element.FirstSibling;
            return true;
        }

        public override bool MoveToFirstAttribute()
        {
            if (!(current is XPathDothtmlElement element) || element.Attributes.IsDefaultOrEmpty)
            {
                return false;
            }
            current = element.Attributes[0];
            return true;
        }

        public override bool MoveToFirstChild()
        {
            if (!(current is XPathDothtmlElement element) || element.Children.IsDefaultOrEmpty)
            {
                return false;
            }
            current = element.Children[0];
            return true;
        }

        public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope) => false;

        public override bool MoveToId(string id) => false;

        public override bool MoveToNext()
        {
            if (!(current is XPathDothtmlElement element) || element.NextSibling == null)
            {
                return false;
            }
            current = element.NextSibling;
            return true;
        }

        public override bool MoveToNextAttribute()
        {
            if (!(current is XPathDothtmlAttribute attribute) || attribute.NextSibling == null)
            {
                return false;
            }
            current = attribute.NextSibling;
            return true;
        }

        public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope) => false;

        public override bool MoveToParent()
        {
            if (current.Parent != null)
            {
                current = current.Parent;
                return true;
            }
            return false;
        }

        public override bool MoveToPrevious()
        {
            if (!(current is XPathDothtmlElement element) || element.PreviousSibling == null)
            {
                return false;
            }
            current = element.PreviousSibling;
            return true;
        }

        public override void MoveToRoot()
        {
            current = current.Root;
        }
    }
}