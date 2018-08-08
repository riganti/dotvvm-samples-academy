using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.Unit
{
    public static class ConstraintContextExtensions
    {
        public static void Report<TResult>(
            this IConstraintContext<TResult> context,
            string message,
            int start = -1,
            int end = -1,
            object underlyingObject = null,
            ValidationSeverity severity = default)
        {
            var reporter = context.Provider.GetRequiredService<ValidationReporter>();
            reporter.Report(message, start, end, underlyingObject, severity);
        }

        public static void Report<TResult>(this IConstraintContext<TResult> context, IValidationDiagnostic diagnostic)
        {
            var reporter = context.Provider.GetRequiredService<ValidationReporter>();
            reporter.Report(diagnostic);
        }
    }
}