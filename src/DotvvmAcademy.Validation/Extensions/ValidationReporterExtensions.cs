using System;

namespace DotvvmAcademy.Validation
{
    public static class ValidationReporterExtensions
    {
        public static void Report(
            this IValidationReporter reporter,
            string message,
            ValidationSeverity severity = default)
        {
            reporter.Report(new GlobalValidationDiagnostic(message, severity));
        }
    }
}