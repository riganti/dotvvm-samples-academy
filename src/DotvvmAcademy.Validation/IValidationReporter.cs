using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public interface IValidationReporter
    {
        SourceCodeStorage SourceCodeStorage { get; }

        IEnumerable<IValidationDiagnostic> GetDiagnostics();

        ValidationSeverity GetWorstSeverity();

        void Report(IValidationDiagnostic diagnostic);
    }
}