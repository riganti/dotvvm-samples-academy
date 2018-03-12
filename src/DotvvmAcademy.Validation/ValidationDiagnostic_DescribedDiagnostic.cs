using System.Collections.Immutable;

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
                MessageArgs = messageArgs;
            }

            public ValidationDiagnosticDescriptor Descriptor { get; }

            public override string Id => Descriptor.Id;

            public override ValidationDiagnosticLocation Location { get; }

            public override string Message => Descriptor.Message;

            public override ImmutableArray<object> MessageArgs { get; }

            public override ValidationDiagnosticSeverity Severity => Descriptor.Severity;

            public override string Name => Descriptor.Name;
        }
    }
}