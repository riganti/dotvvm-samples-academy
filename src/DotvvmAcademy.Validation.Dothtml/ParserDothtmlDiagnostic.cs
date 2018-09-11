using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class ParserDothtmlDiagnostic : IValidationDiagnostic
    {
        public ParserDothtmlDiagnostic(
            string message,
            IEnumerable<object> arguments,
            DothtmlNode node,
            DothtmlSourceCode source,
            ValidationSeverity severity)
        {
            Message = message;
            Arguments = arguments;
            Severity = severity;
            Node = node;
            Source = source;
        }

        public IEnumerable<object> Arguments { get; }

        public string Message { get; }

        public DothtmlNode Node { get; }

        public ValidationSeverity Severity { get; }

        public DothtmlSourceCode Source { get; }

        public int End => Node.EndPosition;

        ISourceCode IValidationDiagnostic.Source => Source;

        public int Start => Node.StartPosition;

        object IValidationDiagnostic.UnderlyingObject => Node;
    }
}