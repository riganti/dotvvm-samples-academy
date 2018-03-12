using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation
{
    public abstract partial class ValidationDiagnostic
    {
        public abstract string Id { get; }

        public abstract ValidationDiagnosticLocation Location { get; }

        public abstract string Message { get; }

        public abstract string Name { get; }

        public abstract ValidationDiagnosticSeverity Severity { get; }

        public static ValidationDiagnostic Create(
            ValidationDiagnosticDescriptor descriptor,
            ValidationDiagnosticLocation location = null,
            ImmutableArray<object> messageArgs = default(ImmutableArray<object>))
        {
            location = location ?? ValidationDiagnosticLocation.Global;
            return new DescribedDiagnostic(descriptor, location, messageArgs);
        }

        public static ValidationDiagnostic Create(
            ValidationDiagnosticDescriptor descriptor,
            ValidationDiagnosticLocation location = null,
            IEnumerable<object> messageArgs = null)
        {
            var array = messageArgs == null
                ? default(ImmutableArray<object>)
                : messageArgs.ToImmutableArray();
            return Create(descriptor, location, array);
        }

        public static ValidationDiagnostic Create(
            ValidationDiagnosticDescriptor descriptor,
            ValidationDiagnosticLocation location = null,
            params object[] messageArgs)
        {
            return Create(descriptor, location, messageArgs.ToImmutableArray());
        }
    }
}