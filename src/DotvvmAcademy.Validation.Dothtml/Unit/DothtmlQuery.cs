using DotvvmAcademy.Validation.Unit;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlQuery<TResult> : Query<TResult>
    {
        public DothtmlQuery(DothtmlUnit unit, XPathExpression expression) : base(unit, expression.Expression)
        {
            Unit = unit;
            Expression = expression;
        }

        public XPathExpression Expression { get; }

        public new DothtmlUnit Unit { get; }
    }
}