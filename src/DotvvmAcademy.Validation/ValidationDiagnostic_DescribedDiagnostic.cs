using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation
{
    public abstract partial class ValidationDiagnostic
    {
        private class DescribedDiagnostic : ValidationDiagnostic
        {
            public DescribedDiagnostic(
                ValidationDiagnosticDescriptor descriptor,
                ValidationDiagnosticLocation location,
                ImmutableArray<object> messageArgs)
            {
                Descriptor = descriptor;
                Location = location;
                // yes, it's ugly and horrifying but right now it's the only thing that works (anywhere)
                Message = string.Format(descriptor.Message, messageArgs.CastArray<object>().ToArray());
            }

            public ValidationDiagnosticDescriptor Descriptor { get; }

            public override string Id => Descriptor.Id;

            public override ValidationDiagnosticLocation Location { get; }

            public override string Message { get; }

            public override ValidationDiagnosticSeverity Severity => Descriptor.Severity;

            public override string Name => Descriptor.Name;
        }
    }
}