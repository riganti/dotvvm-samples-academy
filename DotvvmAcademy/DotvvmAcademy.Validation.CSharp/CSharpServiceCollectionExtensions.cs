using DotvvmAcademy.Validation.CSharp.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class CSharpServiceCollectionExtensions
    {
        public static void AddCSharpValidation(this IServiceCollection collection)
        {
            collection.AddTransient<ICSharpValidatorBuilder, DefaultCSharpValidatorBuilder>();
            collection.AddSingleton<ICSharpValidationRequestFactory, DefaultCSharpValidationRequestFactory>();
        }

        public static void AddCSharpValidationInternalServices(this IServiceCollection collection)
        {
            collection.AddSingleton<ICSharpFullNameProvider, DefaultCSharpFullNameProvider>();
            collection.AddScoped<ICSharpFactory, DefaultCSharpFactory>();
            collection.AddScoped<ICSharpDocument, DefaultCSharpDocument>();
            collection.AddTransient<ICSharpNamespace, DefaultCSharpNamespace>();
            collection.AddTransient<ICSharpClass, DefaultCSharpClass>();
            collection.AddTransient<ICSharpMethod, DefaultCSharpMethod>();
            collection.AddTransient<ICSharpProperty, DefaultCSharpProperty>();
        }
    }
}