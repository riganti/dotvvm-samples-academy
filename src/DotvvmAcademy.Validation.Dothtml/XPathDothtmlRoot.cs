using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class XPathDothtmlRoot : XPathDothtmlNode
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