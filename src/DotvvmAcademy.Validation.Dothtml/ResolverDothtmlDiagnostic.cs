using System.Collections.Generic;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class ResolverDothtmlDiagnostic : IValidationDiagnostic
    {
        public ResolverDothtmlDiagnostic(
            string message,
            IEnumerable<object> arguments,
            IAbstractTreeNode node,
            DothtmlSourceCode source,
            ValidationSeverity severity)
        {
            Message = message;
            Arguments = arguments;
            Severity = severity;
            Node = node;
            Source = source;
            Start = Node?.DothtmlNode?.StartPosition ?? -1;
            End = Node?.DothtmlNode?.EndPosition ?? -1;
        }

        public IEnumerable<object> Arguments { get; }

        public string Message { get; }

        public IAbstractTreeNode Node { get; }

        public ValidationSeverity Severity { get; }

        public DothtmlSourceCode Source { get; }

        public int End { get; }

        ISourceCode IValidationDiagnostic.Source => Source;

        public int Start { get; }

        object IValidationDiagnostic.UnderlyingObject => Node;
    }
}
