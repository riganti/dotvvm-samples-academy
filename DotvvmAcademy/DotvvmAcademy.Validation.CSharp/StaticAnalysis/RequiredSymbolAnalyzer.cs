using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class RequiredSymbolAnalyzer : ValidationAnalyzer
    {
        private List<string> foundRequiredSymbols = new List<string>();
        private ImmutableDictionary<string, RequiredSymbolMetadata> requiredSymbols;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(
            DiagnosticDescriptors.MissingSymbol, DiagnosticDescriptors.RedundantSymbol);

        public override void Initialize(AnalysisContext context)
        {
            requiredSymbols = StaticAnalysis.GetMetadata<RequiredSymbolMetadata>();
            foundRequiredSymbols.Clear();
            context.RegisterSyntaxNodeAction(ValidateNode, SyntaxKindPresets.Declarations);
            context.RegisterCompilationAction(ValidateCompilation);
        }

        private void ValidateCompilation(CompilationAnalysisContext context)
        {
            var missingSymbols = requiredSymbols.Keys.Except(foundRequiredSymbols);
            foreach (string missingSymbol in missingSymbols)
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.MissingSymbol, Location.None, missingSymbol));
            }
        }

        private void ValidateNode(SyntaxNodeAnalysisContext context)
        {
            if (!StaticAnalysis.ValidatedTrees.Contains(context.Node.SyntaxTree))
            {
                return;
            }

            var fullName = context.ContainingSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var requiredSymbol = requiredSymbols.GetValueOrDefault(fullName);
            if (requiredSymbol == null || requiredSymbol.PossibleKind.All(k=>!context.Node.IsKind(k)))
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.RedundantSymbol, context.Node.GetLocation(), fullName));
            }
        }
    }
}