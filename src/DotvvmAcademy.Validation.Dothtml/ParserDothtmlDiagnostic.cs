using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class ParserDothtmlDiagnostic : IValidationDiagnostic
    {
        public ParserDothtmlDiagnostic(
            string message,
            ValidationSeverity severity = ValidationSeverity.Error,
            DothtmlNode node = null)
        {
            Message = message;
            Severity = severity;
            Node = node;
        }

        public string Message { get; }

        public DothtmlNode Node { get; }

        public ValidationSeverity Severity { get; }

        public int Start => Node.StartPosition;

        public int End => Node.EndPosition;

        object IValidationDiagnostic.UnderlyingObject => Node;
    }
}