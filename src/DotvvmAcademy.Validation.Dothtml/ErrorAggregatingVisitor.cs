using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class ErrorAggregatingVisitor : ResolvedControlTreeVisitor
    {
        private readonly IValidationReporter reporter;

        public ErrorAggregatingVisitor(IValidationReporter reporter)
        {
            this.reporter = reporter;
        }

        public override void DefaultVisit(IResolvedTreeNode resolvedNode)
        {
            base.DefaultVisit(resolvedNode);
            if (resolvedNode is not IAbstractTreeNode node)
            {
                return;
            }

            foreach (var error in node.DothtmlNode.NodeErrors)
            {
                reporter.Report(
                    message: error,
                    node: node.DothtmlNode,
                    fileName: node.TreeRoot.FileName,
                    severity: ValidationSeverity.Error);
            }
            foreach (var warning in node.DothtmlNode.NodeWarnings)
            {
                reporter.Report(
                    message: warning,
                    node: node.DothtmlNode,
                    fileName: node.TreeRoot.FileName,
                    severity: ValidationSeverity.Warning);
            }
        }
    }
}
