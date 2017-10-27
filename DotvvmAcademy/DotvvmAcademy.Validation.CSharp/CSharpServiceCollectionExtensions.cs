using DotvvmAcademy.Validation.CSharp.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class CSharpServiceCollectionExtensions
    {
        public static void AddCSharpValidation(this ServiceCollection collection)
        {
            collection.AddTransient<ICSharpValidatorBuilder, DefaultCSharpValidatorBuilder>();
        }
    }
}