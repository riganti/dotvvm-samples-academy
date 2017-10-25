using DotvvmAcademy.Validation.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationResponse : IValidationResponse
    {
        public List<ValidationDiagnostic> Diagnostics { get; } = new List<ValidationDiagnostic>();
    }
}
