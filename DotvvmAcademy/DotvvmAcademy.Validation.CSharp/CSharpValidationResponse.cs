using DotvvmAcademy.Validation.Abstractions;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationResponse : IValidationResponse
    {
        public ImmutableArray<ValidationDiagnostic> Diagnostics { get; set; } = new ImmutableArray<ValidationDiagnostic>();

        public Assembly Assembly { get; set; }
    }
}