using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public class SymbolCSharpDiagnostic : IValidationDiagnostic
    {
        public SymbolCSharpDiagnostic(
            string message,
            int start = -1,
            int end = -1,
            ISymbol symbol = null,
            ValidationSeverity severity = default)
        {
            Message = message;
            Start = start;
            End = end;
            Symbol = symbol;
            Severity = severity;
        }

        public int End { get; }

        public string Message { get; }

        public ValidationSeverity Severity { get; }

        public int Start { get; }

        public ISymbol Symbol { get; }

        object IValidationDiagnostic.UnderlyingObject => Symbol;
    }
}