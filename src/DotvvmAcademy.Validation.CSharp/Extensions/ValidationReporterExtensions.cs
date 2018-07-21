using DotvvmAcademy.Validation.CSharp;
using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation
{
    public static class ValidationReporterExtensions
    {
        public static void Report(this ValidationReporter reporter, Diagnostic diagnostic)
            => reporter.Report(new CompilationCSharpDiagnostic(diagnostic));

        public static void Report(
            this ValidationReporter reporter,
            string message,
            int start = -1,
            int end = -1,
            ISymbol symbol = null,
            ValidationSeverity severity = default)
        {
            reporter.Report(new SymbolCSharpDiagnostic(message, start, end, symbol, severity));
        }
    }
}