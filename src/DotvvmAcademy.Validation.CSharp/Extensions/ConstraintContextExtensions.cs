using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public static class ConstraintContextExtensions
    {
        public static void Report<TResult>(this IConstraintContext<TResult> context, Diagnostic diagnostic)
        {
            var reporter = context.Provider.GetRequiredService<ValidationReporter>();
            reporter.Report(diagnostic);
        }

        public static void Report<TResult>(
            this IConstraintContext<TResult> context,
            string message,
            int start = -1,
            int end = -1,
            ISymbol symbol = null,
            ValidationSeverity severity = default)
        {
            var reporter = context.Provider.GetRequiredService<ValidationReporter>();
            reporter.Report(message, start, end, symbol, severity);
        }
    }
}