using DotvvmAcademy.Validation.Dothtml.ValidationTree;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlDiagnostic : IValidationDiagnostic
    {
        public DothtmlDiagnostic(
            string message,
            ValidationSeverity severity = ValidationSeverity.Error,
            ValidationTreeNode node = null)
        {
            Message = message;
            Severity = severity;
            Node = node;
        }

        public string Message { get; }

        public ValidationTreeNode Node { get; }

        public ValidationSeverity Severity { get; }

        public int End => Node?.DothtmlNode.EndPosition ?? -1;

        public int Start => Node?.DothtmlNode.StartPosition ?? -1;

        object IValidationDiagnostic.UnderlyingObject => Node;
    }
}