using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public interface IValidationResponse
    {
        IEnumerable<ValidationDiagnostic> Diagnostics { get; }
    }
}