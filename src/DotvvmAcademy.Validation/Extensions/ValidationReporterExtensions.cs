using System;

namespace DotvvmAcademy.Validation
{
    public static class ValidationReporterExtensions
    {
        public static void Report(
            this ValidationReporter reporter,
            string message,
            int start,
            int end,
            object underlyingObject = null,
            ValidationSeverity severity = default)
        {
            reporter.Report(new ValidationDiagnostic(message, start, end, underlyingObject, severity));
        }

        public static void Report(
            this ValidationReporter reporter,
            Exception exception,
            int start,
            int end,
            ValidationSeverity severity = default)
        {
            reporter.Report(new ExceptionValidationDiagnostic(exception, start, end, severity));
        }

        public static void ReportGlobal(
            this ValidationReporter reporter,
            string message,
            object underlyingObject = null,
            ValidationSeverity severity = default)
        {
            reporter.Report(new ValidationDiagnostic(message, -1, -1, underlyingObject, severity));
        }

        public static void ReportGlobal(
            this ValidationReporter reporter,
            Exception exception,
            ValidationSeverity severity = default)
        {
            reporter.Report(new ExceptionValidationDiagnostic(exception, -1, -1, severity));
        }
    }
}