using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathDothtmlBindingAttribute : XPathDothtmlAttribute
    {
        public XPathDothtmlBindingAttribute(ResolvedPropertyBinding setter) : base(setter, XPathNodeType.Attribute)
        {
        }

        public override object Value => ((ResolvedPropertyBinding)UnderlyingObject).Binding;
    }
}