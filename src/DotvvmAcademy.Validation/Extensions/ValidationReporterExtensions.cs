using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation
{
    public static class ValidationReporterExtensions
    {
        public static void Report(
            this IValidationReporter reporter,
            string message,
            IEnumerable<object> arguments,
            ValidationSeverity severity = default)
        {
            reporter.Report(new GlobalValidationDiagnostic(message, arguments, severity));
        }

        public static void Report(
            this IValidationReporter reporter,
            string message,
            ValidationSeverity severity = default)
        {
            reporter.Report(new GlobalValidationDiagnostic(message, Enumerable.Empty<object>(), severity));
        }
    }
}