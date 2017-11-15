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

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(
            DiagnosticDescriptors.MissingSymbol, DiagnosticDescriptors.RedundantSymbol);

        public override void Initialize(AnalysisContext context)
        {
            base.Initialize(context);
            if (Metadata == null) return;

            foundRequiredSymbols.Clear();
            foundRequiredSymbols.Add(CSharpConstants.GlobalNamespaceName);
            context.RegisterCompilationStartAction(c =>
            {
                c.RegisterSyntaxNodeAction(ValidateNode, SyntaxKindPresets.Declarations);
                c.RegisterCompilationEndAction(ValidateCompilation);
            });
        }

        private void ValidateCompilation(CompilationAnalysisContext context)
        {
            var missingSymbols = Metadata.Pairs.Keys.Except(foundRequiredSymbols);
            foreach (string missingSymbol in missingSymbols)
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.MissingSymbol, Location.None, missingSymbol));
            }
        }

        private void ValidateNode(SyntaxNodeAnalysisContext context)
        {
            if (!Request.FileTable.Values.Contains(context.Node.SyntaxTree))
            {
                return;
            }
            var fullName = context.ContainingSymbol.ToDisplayString(CommonDisplayFormat);
            var requiredSymbol = (RequiredSymbolMetadata)Metadata.Pairs.GetValueOrDefault(fullName);
            if (requiredSymbol == null || requiredSymbol.PossibleKind.All(k => !context.Node.IsKind(k)))
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.RedundantSymbol, context.Node.GetLocation(), fullName));
            }
            else
            {
                foundRequiredSymbols.Add(fullName);
            }
        }
    }
}