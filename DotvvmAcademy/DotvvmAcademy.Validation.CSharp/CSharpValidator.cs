using DotvvmAcademy.Validation.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidator : IValidator<CSharpValidationRequest, CSharpValidationResponse>
    {
        private readonly ConcurrentDictionary<string, MethodInfo> validationMethods;
        private readonly Action<CSharpValidationContext> defaultAction;

        public CSharpValidator(ConcurrentDictionary<string, MethodInfo> validationMethods, Action<CSharpValidationContext> defaultAction)
        {
            this.validationMethods = validationMethods;
            this.defaultAction = defaultAction;
        }

        public CSharpValidationResponse Validate(CSharpValidationRequest request)
        {
            var context = new CSharpValidationContext(request);

            foreach (var )
            {

            }
        }
    }
}