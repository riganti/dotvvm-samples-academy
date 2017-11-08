using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AllowedSymbolAnalyzer : ValidationAnalyzer
    {
        private ImmutableDictionary<string, AllowedSymbolMetadata> metadata;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(DiagnosticDescriptors.DisallowedSymbol);

        public override void Initialize(AnalysisContext context)
        {
            metadata = Request.StaticAnalysis.GetMetadata<AllowedSymbolMetadata>();
            context.RegisterSyntaxNodeAction(ValidateNode, ImmutableArray.Create(SyntaxKind.IdentifierName));
        }

        private void ValidateNode(SyntaxNodeAnalysisContext context)
        {
            var fullName = context.ContainingSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            if(!metadata.TryGetKey(fullName, out var _))
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.DisallowedSymbol, context.Node.GetLocation(), fullName));
            }
        }
    }
}