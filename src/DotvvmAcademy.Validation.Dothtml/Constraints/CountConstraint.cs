using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Constraints
{
    internal class CountConstraint<TNode>
        where TNode : ValidationTreeNode
    {
        public CountConstraint(XPathExpression expression, int count)
        {
            Expression = expression;
            Count = count;
        }

        public int Count { get; }

        public XPathExpression Expression { get; }

        public void Validate(IValidationReporter reporter, NodeLocator locator)
        {
            var nodes = locator.Locate<TNode>(Expression)
                .ToArray();

            // Correct count
            if (nodes.Length == Count)
            {
                return;
            }

            // Incorrect positive count
            if (nodes.Length > 0)
            {
                foreach (var node in nodes)
                {
                    reporter.Report(
                        message: Resources.ERR_WrongCount,
                        arguments: new object[] { Count, node },
                        node: node);
                }
                return;
            }

            // Incorrect zero count with logical parent
            var parentExpression = Expression.GetLogicalParent();
            if (parentExpression != null)
            {
                var parents = locator.Locate(parentExpression);
                ReportParentedMissing(reporter, parents);
            }
            else
            {
                // Incorrect zero count without parent
                ReportMissing(reporter);
            }
        }

        private void ReportMissing(IValidationReporter reporter)
        {
            if (typeof(ValidationDirective).IsAssignableFrom(typeof(TNode)))
            {
                reporter.Report(
                    message: Resources.ERR_MissingDirective,
                    arguments: new object[] { Expression.GetDirectiveName() });
            }
            else if (typeof(ValidationControl).IsAssignableFrom(typeof(TNode)))
            {
                reporter.Report(
                    message: Resources.ERR_MissingControl,
                    arguments: new object[] { Expression.GetControlName(), Expression.Expression });
            }
            else if (typeof(ValidationPropertySetter).IsAssignableFrom(typeof(TNode)))
            {
                reporter.Report(
                    message: Resources.ERR_MissingProperty,
                    arguments: new object[] { Expression.GetPropertyName(), Expression.Expression });
            }
            else
            {
                reporter.Report(
                    message: Resources.ERR_MissingNode,
                    arguments: new object[] { Expression.GetLastSegment(), Expression.Expression });
            }
        }

        private void ReportParentedMissing(IValidationReporter reporter, IEnumerable<ValidationTreeNode> parents)
        {
            if (typeof(ValidationControl).IsAssignableFrom(typeof(TNode)))
            {
                foreach (var parent in parents)
                {
                    reporter.Report(
                        message: Resources.ERR_MissingControlLocal,
                        arguments: new object[] { Expression.GetControlName() },
                        node: parent);
                }
            }
            else if (typeof(ValidationPropertySetter).IsAssignableFrom(typeof(TNode)))
            {
                foreach (var parent in parents)
                {
                    reporter.Report(
                        message: Resources.ERR_MissingPropertyLocal,
                        arguments: new object[] { Expression.GetPropertyName() },
                        node: parent);
                }
            }
            else
            {
                foreach (var parent in parents)
                {
                    reporter.Report(
                        message: Resources.ERR_MissingNodeLocal,
                        arguments: new object[] { Expression.GetLastSegment() },
                        node: parent);
                }
            }
        }
    }
}