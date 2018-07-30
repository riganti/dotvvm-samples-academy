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
            ValidationTreeNode node,
            ValidationSeverity severity = default)
            => reporter.Report(new ResolverDothtmlDiagnostic(message, node, severity));

        public static void Report(
            this ValidationReporter reporter,
            string message,
            DothtmlNode node,
            ValidationSeverity severity = default)
            => reporter.Report(new ParserDothtmlDiagnostic(message, node, severity));
    }
}