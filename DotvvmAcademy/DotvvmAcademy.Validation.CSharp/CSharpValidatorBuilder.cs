using DotvvmAcademy.Validation.Abstractions;
using System;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidatorBuilder : IValidatorBuilder<CSharpValidator, CSharpValidationRequest, CSharpValidationResponse>
    {
        public CSharpValidator Build()
        {
            throw new NotImplementedException();
        }

        public void UseValidationMethod(MethodInfo method)
        {
            throw new NotImplementedException();
        }
    }
}