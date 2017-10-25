using DotvvmAcademy.Validation.Abstractions;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationResponse : IValidationResponse
    {
        public List<ValidationDiagnostic> Diagnostics { get; } = new List<ValidationDiagnostic>();

        public Assembly Assembly { get; }
    }
}