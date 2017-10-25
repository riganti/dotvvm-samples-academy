using DotvvmAcademy.Validation.Abstractions;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationRequest : IValidationRequest
    {
        public List<string> ValidationMethods { get; } = new List<string>();
    }
}