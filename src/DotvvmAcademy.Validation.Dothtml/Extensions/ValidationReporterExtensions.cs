using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;

namespace DotvvmAcademy.Validation
{
    public static class ValidationReporterExtensions
    {
        public static void Report(
            this ValidationReporter reporter,
            string message,
            ValidationSeverity severity = ValidationSeverity.Error,
            ValidationTreeNode node = null)
            => reporter.Report(new DothtmlDiagnostic(message, severity, node));
    }
}