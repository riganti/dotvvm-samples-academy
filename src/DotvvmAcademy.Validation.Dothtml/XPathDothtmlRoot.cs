using System.Xml.XPath;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class XPathDothtmlRoot : XPathDothtmlNode
    {
        public XPathDothtmlRoot(IAbstractTreeRoot root) : base(root, XPathNodeType.Root)
        {
            Root = this;
        }

        public override string ToString()
        {
            return "Root";
        }
    }
}
