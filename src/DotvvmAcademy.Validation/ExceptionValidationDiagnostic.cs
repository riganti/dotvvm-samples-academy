using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation
{
    public sealed class ExceptionValidationDiagnostic : ValidationDiagnostic
    {
        public ExceptionValidationDiagnostic(ValidationDiagnosticDescriptor descriptor, Exception exception, ImmutableArray<object> messageArgs)
        {
            Descriptor = descriptor;
            Exception = exception;
            Message = string.Format(descriptor.Message, messageArgs.CastArray<object>().ToArray());
        }

        public ExceptionValidationDiagnostic(ValidationDiagnosticDescriptor descriptor, Exception exception, params object[] messageArgs)
            : this(descriptor, exception, messageArgs.ToImmutableArray())
        {
        }

        public ValidationDiagnosticDescriptor Descriptor { get; }

        public Exception Exception { get; }

        public override string Id => Descriptor.Id;

        public override ValidationDiagnosticLocation Location => ValidationDiagnosticLocation.Global;

        public override string Message { get; }

        public override string Name => Descriptor.Name;

        public override ValidationDiagnosticSeverity Severity => Descriptor.Severity;
    }
}