using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public interface IValidationDiagnostic
    {
        IEnumerable<object> Arguments { get; }

        int End { get; }

        string Message { get; }

        ValidationSeverity Severity { get; }

        ISourceCode Source { get; }

        int Start { get; }

        object UnderlyingObject { get; }
    }
}