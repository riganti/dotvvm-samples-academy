using System;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DynamicAnalysisMiddleware : IValidationMiddleware
    {
        public Task InvokeAsync(ValidationContext context, ValidationDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}