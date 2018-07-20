using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
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
            => reporter.Report(new ResolverDothtmlDiagnostic(message, severity, node));

        public static void Report(
            this ValidationReporter reporter,
            string message,
            ValidationSeverity severity = ValidationSeverity.Error,
            DothtmlNode node = null)
            => reporter.Report(new ParserDothtmlDiagnostic(message, severity, node));
    }
}