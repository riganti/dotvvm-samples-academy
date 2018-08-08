using System.Collections.Immutable;

namespace DotvvmAcademy.Validation
{
    public class ValidationReporter
    {
        private ImmutableArray<IValidationDiagnostic>.Builder builder
            = ImmutableArray.CreateBuilder<IValidationDiagnostic>();

        public ImmutableArray<IValidationDiagnostic> GetDiagnostics()
        {
            return builder.ToImmutable();
        }

        public void Report(IValidationDiagnostic diagnostic)
        {
            builder.Add(diagnostic);
        }
    }
}