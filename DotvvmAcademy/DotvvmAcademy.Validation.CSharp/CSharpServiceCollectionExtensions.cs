using DotvvmAcademy.Validation.CSharp.UnitValidation;
using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class CSharpServiceCollectionExtensions
    {
        public static void AddCSharpUnitValidation(this IServiceCollection collection)
        {
            collection.AddSingleton<ICSharpNameFormatter, DefaultCSharpNameFormatter>();
            collection.AddScoped<ICSharpNameStack, DefaultCSharpNameStack>();
            collection.AddScoped<ICSharpObjectFactory, DefaultCSharpObjectFactory>();
            collection.AddScoped<ICSharpDocument, DefaultCSharpDocument>();
            collection.AddTransient<ICSharpNamespace, DefaultCSharpNamespace>();
            collection.AddTransient<ICSharpClass, DefaultCSharpClass>();
            collection.AddTransient<ICSharpMethod, DefaultCSharpMethod>();
            collection.AddTransient<ICSharpProperty, DefaultCSharpProperty>();
        }
    }
}