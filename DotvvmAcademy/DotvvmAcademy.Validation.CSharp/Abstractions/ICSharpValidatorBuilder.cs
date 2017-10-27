using DotvvmAcademy.Validation.Abstractions;
using System;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface ICSharpValidatorBuilder : IValidatorBuilder<ICSharpValidator, CSharpValidationRequest, CSharpValidationResponse>
    {
        void AddValidationMethod(string name, CSharpValidationMethod method);

        void AddValidationMethod(MethodInfo method);

        void AddValidationMethods(Assembly assembly);

        void SetServiceProvider(IServiceProvider provider);
    }
}