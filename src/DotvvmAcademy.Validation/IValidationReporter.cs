using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public interface IValidationReporter
    {
        SourceCodeStorage SourceCodeStorage { get; }

        IEnumerable<IValidationDiagnostic> GetDiagnostics();

        void Report(IValidationDiagnostic diagnostic);
    }
}