using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.Unit
{
    public static class ConstraintContextExtensions
    {
        public static ValidationReporter GetReporter<TResult>(this ConstraintContext<TResult> context)
        {
            return context.Provider.GetRequiredService<ValidationReporter>();
        }
    }
}