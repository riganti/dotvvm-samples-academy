using System.Xml.XPath;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlQuery<TResult>
        where TResult : IAbstractTreeNode
    {
        public DothtmlQuery(DothtmlUnit unit, XPathExpression expression)
        {
            Unit = unit;
            Expression = expression;
        }

        public DothtmlUnit Unit { get; }

        public XPathExpression Expression { get; }

        internal DothtmlQuery<TResult> AddConstraint<TConstraint>(TConstraint constraint, params object[] parameters)
        {
            var queryParameters = new object[parameters.Length + 1];
            queryParameters[0] = Expression.Expression;
            parameters.CopyTo(queryParameters, 1);
            Unit.AddConstraint(constraint, queryParameters);
            return this;
        }
    }
}
