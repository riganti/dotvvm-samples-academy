using DotvvmAcademy.Validation.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpValidatorBuilder : IValidatorBuilder<ICSharpValidator, CSharpValidationRequest, CSharpValidationResponse>
    {
        void RegisterValidationMethod(MethodInfo method);

        void RegisterAssembly(Assembly assembly);

        Action<IServiceCollection> ConfigureServices { get; set; }
    }
}