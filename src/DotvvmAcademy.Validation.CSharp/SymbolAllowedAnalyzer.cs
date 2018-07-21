using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SymbolAllowedAnalyzer : DiagnosticAnalyzer
    {
        public static readonly DiagnosticDescriptor SymbolUsageForbiddenDiagnostic = new DiagnosticDescriptor(
            id: null,
            title: "Symbol Usage Forbidden",
            messageFormat: "Usage of symbol '{0}' is forbidden.",
            category: null,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public SymbolAllowedAnalyzer(ImmutableHashSet<ISymbol> allowed)
        {
            Allowed = allowed;
        }

        public ImmutableHashSet<ISymbol> Allowed { get; }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(SymbolUsageForbiddenDiagnostic);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(ValidateNode, RoslynKinds.Identifiers);
        }

        public void ValidateNode(SyntaxNodeAnalysisContext context)
        {
            var symbol = context.SemanticModel.GetSymbolInfo(context.Node).Symbol;
            if (!Allowed.Contains(symbol))
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    descriptor: SymbolUsageForbiddenDiagnostic,
                    location: context.Node.GetLocation(),
                    messageArgs: symbol.ToDisplayString()));
            }
        }
    }
}