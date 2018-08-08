using System;

namespace DotvvmAcademy.Validation
{
    public static class ValidationReporterExtensions
    {
        public static void Report(
            this ValidationReporter reporter,
            string message,
            int start = -1,
            int end = -1,
            object underlyingObject = null,
            ValidationSeverity severity = default)
        {
            reporter.Report(new ValidationDiagnostic(message, start, end, underlyingObject, severity));
        }

        public static void Report(
            this ValidationReporter reporter,
            Exception exception,
            int start = -1,
            int end = -1,
            ValidationSeverity severity = default)
        {
            reporter.Report(new ExceptionValidationDiagnostic(exception, start, end, severity));
        }
    }
}