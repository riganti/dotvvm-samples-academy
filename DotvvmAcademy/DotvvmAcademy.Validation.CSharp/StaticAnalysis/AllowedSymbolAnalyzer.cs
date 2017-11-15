using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AllowedSymbolAnalyzer : ValidationAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(DiagnosticDescriptors.DisallowedSymbol);

        public override void Initialize(AnalysisContext context)
        {
            base.Initialize(context);
            if(Metadata == null) return;

            context.RegisterSyntaxNodeAction(ValidateNode, SyntaxKindPresets.Identifiers);
        }

        private void ValidateNode(SyntaxNodeAnalysisContext context)
        {
            var info = context.SemanticModel.GetSymbolInfo(context.Node);
            var fullName = info.Symbol.ToDisplayString(CommonDisplayFormat);
            if(!Metadata.FullNames.Contains(fullName))
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.DisallowedSymbol, context.Node.GetLocation(), fullName));
            }
        }
    }
}