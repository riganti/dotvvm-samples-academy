using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation
{
    public interface IValidationResponse
    {
        IEnumerable<ValidationDiagnostic> Diagnostics { get; }
    }
}