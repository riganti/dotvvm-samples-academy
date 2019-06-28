using DotvvmAcademy.Validation.Dothtml;

namespace System.Xml.XPath
{
    public static class XPathExpressionExtensions
    {
        public static XPathExpression GetLogicalParent(this XPathExpression xpath)
        {
            return XPathExpression.Compile(XPathUtilities.GetParentPath(xpath.Expression));
        }
    }
}