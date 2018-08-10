using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class ParserDothtmlDiagnostic : IValidationDiagnostic
    {
        public ParserDothtmlDiagnostic(
            string message,
            DothtmlNode node,
            DothtmlSourceCode source,
            ValidationSeverity severity)
        {
            Message = message;
            Severity = severity;
            Node = node;
            Source = source;
        }

        public string Message { get; }

        public DothtmlNode Node { get; }

        public ValidationSeverity Severity { get; }

        public int Start => Node.StartPosition;

        public int End => Node.EndPosition;

        public DothtmlSourceCode Source { get; }

        ISourceCode IValidationDiagnostic.Source => Source;

        object IValidationDiagnostic.UnderlyingObject => Node;
    }
}