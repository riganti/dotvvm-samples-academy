using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation
{
    public sealed class ExceptionValidationDiagnostic : ValidationDiagnostic
    {
        public ExceptionValidationDiagnostic(ValidationDiagnosticDescriptor descriptor, Exception exception, ImmutableArray<object> messageArgs)
        {
            Descriptor = descriptor;
            Exception = exception;
            MessageArgs = messageArgs;
        }

        public ExceptionValidationDiagnostic(ValidationDiagnosticDescriptor descriptor, Exception exception, params object[] messageArgs)
            : this(descriptor, exception, messageArgs.ToImmutableArray())
        {
        }

        public ValidationDiagnosticDescriptor Descriptor { get; }

        public Exception Exception { get; }

        public override string Id => Descriptor.Id;

        public override ValidationDiagnosticLocation Location => ValidationDiagnosticLocation.Global;

        public override string Message => Descriptor.Message;

        public override ImmutableArray<object> MessageArgs { get; }

        public override string Name => Descriptor.Name;

        public override ValidationDiagnosticSeverity Severity => Descriptor.Severity;
    }
}