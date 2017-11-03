using DotvvmAcademy.Validation.CSharp.Abstractions;
using DotvvmAcademy.Validation.CSharp.Analyzers;
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

        internal static void AddDefaultCSharpValidation(this IServiceCollection collection)
        {
            collection.AddTransient<ICSharpValidator, DefaultCSharpValidator>();
            AddCSharpObjects(collection);
            AddInternalServices(collection);
            AddValidationAnalyzers(collection);
        }

        private static void AddCSharpObjects(IServiceCollection collection)
        {
            collection.AddScoped<ICSharpDocument, DefaultCSharpDocument>();
            collection.AddTransient<ICSharpNamespace, DefaultCSharpNamespace>();
            collection.AddTransient<ICSharpClass, DefaultCSharpClass>();
            collection.AddTransient<ICSharpMethod, DefaultCSharpMethod>();
            collection.AddTransient<ICSharpProperty, DefaultCSharpProperty>();
        }

        private static void AddInternalServices(IServiceCollection collection)
        {
            collection.AddScoped<ICSharpFactory, DefaultCSharpFactory>();
            collection.AddSingleton<ICSharpFullNameProvider, DefaultCSharpFullNameProvider>();
        }

        private static void AddValidationAnalyzers(IServiceCollection collection)
        {
            collection.AddSingleton<ValidationAnalyzer, RequiredSymbolAnalyzer>();
            collection.AddSingleton<ValidationAnalyzer, AccessModifierAnalyzer>();
        }

        private static void AddMetadataExtractors(IServiceCollection collection)
        {
            collection.AddSingleton<IMetadataExtractor, RequiredSymbolMetadataExtractor>();
        }
    }
}