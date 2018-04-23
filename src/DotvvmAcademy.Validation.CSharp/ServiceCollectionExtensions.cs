using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAssemblyRewriting(this IServiceCollection services)
        {
            services.AddScoped<IAssemblyRewriter, DefaultAssemblyRewriter>();
            services.AddTransient<IAssemblySafeguard, DefaultAssemblySafeguard>();

            return services;
        }

        public static IServiceCollection AddDynamicAnalysis(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddStaticAnalysis(this IServiceCollection services)
        {
            services.AddScoped<OldMetadataCollection>();
            services.AddScoped<ValidationDiagnosticAnalyzer, BaseTypeAnalyzer>();
            services.AddScoped<ValidationDiagnosticAnalyzer, DeclarationExistenceAnalyzer>();
            services.AddScoped<ValidationDiagnosticAnalyzer, InterfaceImplementationAnalyzer>();
            services.AddScoped<ValidationDiagnosticAnalyzer, SymbolAccessibilityAnalyzer>();
            services.AddScoped<ValidationDiagnosticAnalyzer, SymbolAllowedAnalyzer>();
            services.AddScoped<ValidationDiagnosticAnalyzer, SymbolStaticAnalyzer>();
            services.AddScoped<ValidationDiagnosticAnalyzer, TypeKindAnalyzer>();

            return services;
        }
    }
}