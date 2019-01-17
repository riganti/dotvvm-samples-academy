using DotvvmAcademy.Validation.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation
{
    public static class ValidationReporterExtensions
    {
        public static void Report(this IValidationReporter reporter, Diagnostic diagnostic)
        {
            var source = reporter.SourceCodeStorage.GetSourceCode(diagnostic.Location.SourceTree);
            reporter.Report(new CompilationCSharpDiagnostic(diagnostic, source));
        }

        public static void Report(this IValidationReporter reporter, string message, ISymbol symbol, ValidationSeverity severity = default)
        {
            reporter.Report(message, Enumerable.Empty<object>(), symbol, severity);
        }

        public static void Report(
            this IValidationReporter reporter,
            string message,
            IEnumerable<object> arguments,
            ISymbol symbol,
            ValidationSeverity severity = default)
        {
            foreach (var reference in symbol.DeclaringSyntaxReferences)
            {
                var source = reporter.SourceCodeStorage.GetSourceCode(reference.SyntaxTree);
                var syntax = reference.GetSyntax();
                var start = reference.Span.Start;
                var end = reference.Span.End;
                switch (syntax)
                {
                    case NamespaceDeclarationSyntax namespaceDeclaration:
                        start = namespaceDeclaration.Name.Span.Start;
                        end = namespaceDeclaration.Name.Span.End;
                        break;

                    case BaseTypeDeclarationSyntax typeDeclaration:
                        start = typeDeclaration.Identifier.Span.Start;
                        end = typeDeclaration.Identifier.Span.End;
                        break;

                    case PropertyDeclarationSyntax propertyDeclaration:
                        start = propertyDeclaration.Identifier.Span.Start;
                        end = propertyDeclaration.Identifier.Span.End;
                        break;

                    case EventDeclarationSyntax eventDeclaration:
                        start = eventDeclaration.Identifier.Span.Start;
                        end = eventDeclaration.Identifier.Span.End;
                        break;
                        // TODO: Eventually handle fields
                }
                reporter.Report(new SymbolCSharpDiagnostic(
                    message: message,
                    arguments: arguments,
                    start: start,
                    end: end,
                    source: source,
                    symbol: symbol,
                    severity: severity));
            }
        }
    }
}