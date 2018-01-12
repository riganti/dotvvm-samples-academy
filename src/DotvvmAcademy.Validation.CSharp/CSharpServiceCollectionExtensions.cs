using DotvvmAcademy.Validation.CSharp.AssemblyAnalysis;
using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.CSharp.Unit.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class CSharpServiceCollectionExtensions
    {
        public static void AddCSharpUnitValidation(this IServiceCollection collection)
        {
            collection.AddSingleton<ICSharpNameFormatter, DefaultCSharpNameFormatter>();
            collection.AddSingleton<IMetadataExtractor, AllowedSymbolMetadataExtractor>();
            collection.AddSingleton<IMetadataExtractor, RequiredSymbolMetadataExtractor>();
            collection.AddSingleton<IMetadataExtractor, AccessModifierMetadataExtractor>();
            collection.AddScoped<ICSharpNameStack, DefaultCSharpNameStack>();
            collection.AddScoped<ICSharpObjectFactory, DefaultCSharpObjectFactory>();
            collection.AddScoped<ICSharpDocument, DefaultCSharpDocument>();
            collection.AddTransient<ICSharpNamespace, DefaultCSharpNamespace>();
            collection.AddTransient<ICSharpClass, DefaultCSharpClass>();
            collection.AddTransient<ICSharpMethod, DefaultCSharpMethod>();
            collection.AddTransient<ICSharpProperty, DefaultCSharpProperty>();
            collection.AddTransient<ICSharpAccessor, DefaultCSharpAccessor>();
        }

        public static void AddCSharpValidationAnalyzers(this IServiceCollection collection)
        {
            collection.AddTransient<ValidationAnalyzer, AllowedSymbolAnalyzer>();
            collection.AddTransient<ValidationAnalyzer, RequiredSymbolAnalyzer>();
            collection.AddTransient<ValidationAnalyzer, AccessModifierAnalyzer>();
            collection.AddSingleton<IAssemblyRewriter, DefaultAssemblyRewriter>();
        }
    }
}