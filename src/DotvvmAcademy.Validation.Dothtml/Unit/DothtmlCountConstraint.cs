using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using System.Collections.Immutable;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlCountConstraint<TResult> : IConstraint
        where TResult : ValidationTreeNode
    {
        public DothtmlCountConstraint(XPathExpression xPath, int count)
        {
            XPath = xPath;
            Count = count;
        }

        public int Count { get; }

        public XPathExpression XPath { get; }

        public bool CanOverwrite(IConstraint other)
        {
            return other is DothtmlCountConstraint<TResult> otherCount
                && XPath.Expression.Equals(otherCount.XPath.Expression);
        }

        public int GetOverwriteHashCode()
        {
            return XPath.Expression.GetHashCode();
        }

        public void Validate(ConstraintContext context)
        {
            var result = context.Locate<TResult>(XPath);

            // Correct count
            if (result.Length == Count)
            {
                return;
            }

            // Incorrect positive count
            if (result.Length > 0)
            {
                foreach (var node in result)
                {
                    context.Report(
                        message: Resources.ERR_WrongCount,
                        arguments: new object[] { Count, node },
                        node: node);
                }
                return;
            }

            // Incorrect zero count with logical parent
            var parentXPath = XPath.GetLogicalParent();
            if (parentXPath != null)
            {
                var parents = context.Locate(parentXPath);
                ReportParentedMissing(context, parents);
                return;
            }

            // Incorrect zero count without parent
            ReportMissing(context);
        }

        private void ReportMissing(ConstraintContext context)
        {
            var parentXPath = XPath.GetLogicalParent();
            if (typeof(ValidationDirective).IsAssignableFrom(typeof(TResult)))
            {
                context.Report(
                    message: Resources.ERR_MissingDirective,
                    arguments: new object[] { XPath.GetDirectiveName() });
                return;
            }

            if (typeof(ValidationControl).IsAssignableFrom(typeof(TResult)))
            {
                context.Report(
                    message: Resources.ERR_MissingControl,
                    arguments: new object[] { XPath.GetControlName(), parentXPath.Expression });
                return;
            }

            if (typeof(ValidationPropertySetter).IsAssignableFrom(typeof(TResult)))
            {
                context.Report(
                    message: Resources.ERR_MissingProperty,
                    arguments: new object[] { XPath.GetPropertyName(), parentXPath.Expression });
                return;
            }

            context.Report(
                message: Resources.ERR_MissingNode,
                arguments: new object[] { XPath.GetLastSegment(), parentXPath.Expression });
        }

        private void ReportParentedMissing(ConstraintContext context, ImmutableArray<ValidationTreeNode> parents)
        {
            if (typeof(ValidationControl).IsAssignableFrom(typeof(TResult)))
            {
                foreach (var parent in parents)
                {
                    context.Report(
                        message: Resources.ERR_MissingControlLocal,
                        arguments: new object[] { XPath.GetControlName() },
                        node: parent);
                }
                return;
            }

            if (typeof(ValidationPropertySetter).IsAssignableFrom(typeof(TResult)))
            {
                foreach (var parent in parents)
                {
                    context.Report(
                        message: Resources.ERR_MissingPropertyLocal,
                        arguments: new object[] { XPath.GetPropertyName() },
                        node: parent);
                }
                return;
            }

            foreach (var parent in parents)
            {
                context.Report(
                    message: Resources.ERR_MissingNodeLocal,
                    arguments: new object[] { XPath.GetLastSegment() },
                    node: parent);
            }
            return;
        }
    }
}