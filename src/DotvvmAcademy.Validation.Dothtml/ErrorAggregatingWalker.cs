using DotvvmAcademy.Validation.Dothtml.ValidationTree;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class ErrorAggregatingWalker : DothtmlTreeWalker
    {
        private readonly IValidationReporter reporter;

        public ErrorAggregatingWalker(IValidationReporter reporter)
        {
            this.reporter = reporter;
        }

        public override void DefaultVisit(ValidationTreeNode node)
        {
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
