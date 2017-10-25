using DotvvmAcademy.Validation.Abstractions;
using System;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidatorBuilder : IValidatorBuilder<CSharpValidator, CSharpValidationRequest, CSharpValidationResponse>
    {
        public CSharpValidator Build()
        {
            var validator = new CSharpValidator()
        }

        public void AddValidationMethod(string name, MethodInfo method)
        {
            throw new NotImplementedException();
        }
    }
}