using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class ValidationReporterExtensions
    {
        public static void Report(this ValidationReporter reporter, Diagnostic diagnostic)
        {
            reporter.Report(new CompilationCSharpDiagnostic(diagnostic));
        }

        public static void Report(
            this ValidationReporter reporter,
            string message,
            int start,
            int end,
            ISymbol symbol,
            ValidationSeverity severity = default)
        {
            reporter.Report(new SymbolCSharpDiagnostic(message, start, end, symbol, severity));
        }

        public static void ReportAllLocations(
            this ValidationReporter reporter,
            string message,
            ISymbol symbol,
            ValidationSeverity severity = default)
        {
            foreach (var declaration in symbol.DeclaringSyntaxReferences)
            {
                reporter.Report(new SymbolCSharpDiagnostic(
                    message: message,
                    start: declaration.Span.Start,
                    end: declaration.Span.End,
                    symbol: symbol,
                    severity: severity));
            }
        }

        public static void ReportGlobal(
            this ValidationReporter reporter,
            string message,
            ISymbol symbol,
            ValidationSeverity severity = default)
        {
            reporter.Report(new SymbolCSharpDiagnostic(message, -1, -1, symbol, severity));
        }
    }
}