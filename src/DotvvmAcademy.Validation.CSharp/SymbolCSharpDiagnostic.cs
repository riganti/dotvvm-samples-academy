using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public class SymbolCSharpDiagnostic : IValidationDiagnostic
    {
        public SymbolCSharpDiagnostic(
            string message,
            int start,
            int end,
            ISymbol symbol,
            ValidationSeverity severity)
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