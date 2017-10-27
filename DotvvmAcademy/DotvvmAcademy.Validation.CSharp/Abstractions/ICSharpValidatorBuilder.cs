using DotvvmAcademy.Validation.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpValidatorBuilder : IValidatorBuilder<ICSharpValidator, CSharpValidationRequest, CSharpValidationResponse>
    {
        void AddValidationMethod(string name, CSharpValidationMethod method);

        void AddValidationMethod(MethodInfo method);

        void AddValidationAssembly(Assembly assembly);

        void ConfigureServiceCollection(Action<IServiceCollection> configureCollection);
    }
}