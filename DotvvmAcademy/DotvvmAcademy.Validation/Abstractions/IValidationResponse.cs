using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Abstractions
{
    public interface IValidationResponse
    {
        List<ValidationDiagnostic> Diagnostics { get; }
    }
}