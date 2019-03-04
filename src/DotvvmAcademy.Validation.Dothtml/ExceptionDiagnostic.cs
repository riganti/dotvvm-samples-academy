using DotVVM.Framework.Compilation;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class ExceptionDiagnostic : IValidationDiagnostic
    {
        public ExceptionDiagnostic(DotvvmCompilationException exception, ISourceCode source)
        {
            Exception = exception;
            Source = source;
            Start = exception.Tokens.First().StartPosition;
            End = exception.Tokens.Last().EndPosition;
        }

        public IEnumerable<object> Arguments { get; } = Enumerable.Empty<object>();

        public int End { get; }

        public DotvvmCompilationException Exception { get; }

        public ValidationSeverity Severity { get; } = ValidationSeverity.Error;

        public ISourceCode Source { get; }

        public int Start { get; }

        public object UnderlyingObject => Exception;

        public string Message => Exception.InnerException.Message;
    }
}