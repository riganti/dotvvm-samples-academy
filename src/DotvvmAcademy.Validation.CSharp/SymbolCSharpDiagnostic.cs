using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public class SymbolCSharpDiagnostic : IValidationDiagnostic
    {
        public SymbolCSharpDiagnostic(
            string message,
            int start,
            int end,
            CSharpSourceCode source,
            ISymbol symbol,
            ValidationSeverity severity)
        {
            Message = message;
            Start = start;
            End = end;
            Source = source;
            Symbol = symbol;
            Severity = severity;
        }

        public int End { get; }

        public string Message { get; }

        public ValidationSeverity Severity { get; }

        public int Start { get; }

        public ISymbol Symbol { get; }

        public CSharpSourceCode Source { get; }

        ISourceCode IValidationDiagnostic.Source => Source;

        object IValidationDiagnostic.UnderlyingObject => Symbol;
    }
}