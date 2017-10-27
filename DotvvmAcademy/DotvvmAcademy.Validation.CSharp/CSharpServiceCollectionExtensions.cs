using DotvvmAcademy.Validation.CSharp.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class CSharpServiceCollectionExtensions
    {
        public static void AddCSharpValidation(this IServiceCollection collection)
        {
            collection.AddTransient<ICSharpValidatorBuilder, DefaultCSharpValidatorBuilder>();
        }

        public static void AddCSharpValidationInternalServices(this IServiceCollection collection)
        {
            collection.AddScoped<ICSharpFactory, DefaultCSharpFactory>();
            collection.AddTransient<ICSharpDocument, DefaultCSharpDocument>();
        }
    }
}