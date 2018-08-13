using DotvvmAcademy.Validation.Dothtml.ValidationTree;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class ResolverDothtmlDiagnostic : IValidationDiagnostic
    {
        public ResolverDothtmlDiagnostic(
            string message,
            ValidationTreeNode node,
            DothtmlSourceCode source,
            ValidationSeverity severity)
        {
            Message = message;
            Severity = severity;
            Node = node;
            Source = source;
        }

        public string Message { get; }

        public ValidationTreeNode Node { get; }

        public ValidationSeverity Severity { get; }

        public int End => Node?.DothtmlNode.EndPosition ?? -1;

        public int Start => Node?.DothtmlNode.StartPosition ?? -1;

        public DothtmlSourceCode Source { get; }

        ISourceCode IValidationDiagnostic.Source => Source;

        object IValidationDiagnostic.UnderlyingObject => Node;
    }
}