using DotVVM.Framework.Compilation.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class TokenErrorDiagnostic : IValidationDiagnostic
    {
        public TokenErrorDiagnostic(TokenError error, DothtmlSourceCode source, ValidationSeverity severity)
        {
            Error = error;
            Source = source;
            Severity = severity;

            UnderlyingObject = Error;
            Message = Error.ErrorMessage;
            Start = Error.Range.StartPosition;
            End = Error.Range.EndPosition;
        }

        public IEnumerable<object> Arguments { get; } = Enumerable.Empty<object>();

        public int End { get; }

        public string Message { get; }

        public ValidationSeverity Severity { get; }

        public TokenError Error { get; }

        public ISourceCode Source { get; }

        public int Start { get; }

        public object UnderlyingObject { get; }
    }
}
