using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public class SymbolCSharpDiagnostic : IValidationDiagnostic
    {
        public SymbolCSharpDiagnostic(
            string message,
            IEnumerable<object> arguments,
            int start,
            int end,
            CSharpSourceCode source,
            ISymbol symbol,
            ValidationSeverity severity)
        {
            Message = message;
            Arguments = arguments;
            Start = start;
            End = end;
            Source = source;
            Symbol = symbol;
            Severity = severity;
        }

        public IEnumerable<object> Arguments { get; }

        public int End { get; }

        public string Message { get; }

        public ValidationSeverity Severity { get; }

        public CSharpSourceCode Source { get; }

        public int Start { get; }

        public ISymbol Symbol { get; }

        ISourceCode IValidationDiagnostic.Source => Source;

        object IValidationDiagnostic.UnderlyingObject => Symbol;
    }
}