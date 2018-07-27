using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathDothtmlRoot : XPathDothtmlNode
    {
        public XPathDothtmlRoot(ValidationTreeRoot root) : base(root, XPathNodeType.Root)
        {
            Root = this;
        }

        public override string ToString()
        {
            return "Root";
        }
    }
}