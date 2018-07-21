using System.Collections.Immutable;

namespace DotvvmAcademy.Validation
{
    public class ValidationReporter
    {
        private ImmutableArray<IValidationDiagnostic>.Builder builder
            = ImmutableArray.CreateBuilder<IValidationDiagnostic>();

        public int Count => builder.Count;

        public ImmutableArray<IValidationDiagnostic> GetDiagnostics()
            => builder.ToImmutable();

        public void Report(IValidationDiagnostic diagnostic) => builder.Add(diagnostic);

        public void Report(
            string message,
            int start = -1,
            int end = -1,
            object underlyingObject = null,
            ValidationSeverity severity = default)
            => Report(new ValidationDiagnostic(message, start, end, underlyingObject, severity));
    }
}