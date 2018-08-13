using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.Unit
{
    public static class QueryExtensions
    {
        public static IQuery<TResult> CountEquals<TResult>(this IQuery<TResult> query, int count)
        {
            query.SetConstraint(nameof(CountEquals), context =>
            {
                if (context.Result.Length != count)
                {
                    context.Provider
                    .GetRequiredService<IValidationReporter>()
                    .Report($"Found '{context.Result.Length}' of '{context.Query.Source}' " +
                            $"but expected to find '{count}'.");
                }
            });
            return query;
        }
    }
}