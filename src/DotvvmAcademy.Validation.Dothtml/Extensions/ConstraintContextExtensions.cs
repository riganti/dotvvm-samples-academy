using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class ConstraintContextExtensions
    {
        public static void Report<TResult>(
            this IConstraintContext<TResult> context,
            string message,
            ValidationTreeNode node,
            ValidationSeverity severity = default)
        {
            var reporter = context.Provider.GetRequiredService<ValidationReporter>();
            reporter.Report(message, node, severity);
        }

        public static void Report<TResult>(
            this IConstraintContext<TResult> context,
            string message,
            DothtmlNode node,
            ValidationSeverity severity = default)
        {
            var reporter = context.Provider.GetRequiredService<ValidationReporter>();
            reporter.Report(message, node, severity);
        }
    }
}