using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public class ValidationReporter : IValidationReporter
    {
        private readonly List<IValidationDiagnostic> diagnostics = new List<IValidationDiagnostic>();

        public ValidationReporter(SourceCodeStorage sourceCodeStorage)
        {
            SourceCodeStorage = sourceCodeStorage;
        }

        public SourceCodeStorage SourceCodeStorage { get; }

        public IEnumerable<IValidationDiagnostic> GetDiagnostics()
        {
            return diagnostics;
        }

        public void Report(IValidationDiagnostic diagnostic)
        {
            diagnostics.Add(diagnostic);
        }
    }
}