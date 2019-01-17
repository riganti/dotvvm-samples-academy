using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlQuery<TResult>
        where TResult : ValidationTreeNode
    {
        public DothtmlQuery(DothtmlUnit unit, XPathExpression expression)
        {
            Unit = unit;
            Expression = expression;
        }

        public DothtmlUnit Unit { get; }

        public XPathExpression Expression { get; }
    }
}