using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpValidationReporter : ValidationReporter
    {
        private readonly CSharpSourceCodeProvider sourceCodeProvider;

        public CSharpValidationReporter(CSharpSourceCodeProvider sourceCodeProvider)
        {
            this.sourceCodeProvider = sourceCodeProvider;
        }

        public void Report(Diagnostic diagnostic)
        {
            var source = sourceCodeProvider.GetSourceCode(diagnostic.Location.SourceTree);
            Report(new CompilationCSharpDiagnostic(diagnostic, source));
        }

        public void Report(string message, ISymbol symbol, ValidationSeverity severity = default)
        {
            foreach (var reference in symbol.DeclaringSyntaxReferences)
            {
                var source = sourceCodeProvider.GetSourceCode(reference.SyntaxTree);
                Report(new SymbolCSharpDiagnostic(
                    message: message,
                    start: reference.Span.Start,
                    end: reference.Span.End,
                    source: source,
                    symbol: symbol,
                    severity: severity));
            }
        }
    }
}