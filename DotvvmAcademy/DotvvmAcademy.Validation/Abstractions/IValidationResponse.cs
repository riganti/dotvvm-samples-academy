using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Abstractions
{
    public interface IValidationResponse
    {
        ImmutableArray<ValidationDiagnostic> Diagnostics { get; }
    }
}