using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.Unit
{
    public static class ConstraintContextExtensions
    {
        public static void Report(
            this ConstraintContext context,
            string message,
            ValidationSeverity severity = default)
        {
            context.Provider.GetRequiredService<IValidationReporter>().Report(message, Enumerable.Empty<object>(), severity);
        }

        public static void Report(
            this ConstraintContext context,
            string message,
            IEnumerable<object> arguments,
            ValidationSeverity severity = default)
        {
            context.Provider.GetRequiredService<IValidationReporter>().Report(message, arguments, severity);
        }
    }
}