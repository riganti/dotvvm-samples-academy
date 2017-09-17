using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AllowedIdentifierAnalyzer : DiagnosticAnalyzer
    {
        private DiagnosticDescriptor identifierNotAllowedDiagnosticDescriptor = new DiagnosticDescriptor("DotvvmAcademy0001", "A disallowed identifier is being used.", "Identifier '{0}' is not allowed in this exercise.", "DotvvmAcademy.Validation", DiagnosticSeverity.Error, true);
        private ImmutableArray<DiagnosticDescriptor> supportedDiagnostics;

        public AllowedIdentifierAnalyzer(ImmutableArray<ISymbol> allowedSymbols)
        {
            supportedDiagnostics = new ImmutableArray<DiagnosticDescriptor> { identifierNotAllowedDiagnosticDescriptor };
            AllowedSymbols = allowedSymbols;
        }

        public ImmutableArray<ISymbol> AllowedSymbols { get; }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => supportedDiagnostics;

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(VisitIdentifier, SyntaxKind.IdentifierToken);
        }

        private void VisitIdentifier(SyntaxNodeAnalysisContext context)
        {
            if (!AllowedSymbols.Contains(context.ContainingSymbol))
            {
                context.ReportDiagnostic(Diagnostic.Create(identifierNotAllowedDiagnosticDescriptor, context.Node.GetLocation(), context.ContainingSymbol.Name));
            }
        }
    }
}