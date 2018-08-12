using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SymbolAllowedAnalyzer : DiagnosticAnalyzer
    {
        public static readonly DiagnosticDescriptor SymbolUsageForbidden = new DiagnosticDescriptor(
            id: nameof(SymbolUsageForbidden),
            title: "Symbol Usage Forbidden",
            messageFormat: "Usage of symbol '{0}' is forbidden.",
            category: nameof(DotvvmAcademy),
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private readonly AllowedSymbolStorage storage;

        public SymbolAllowedAnalyzer(AllowedSymbolStorage storage)
        {
            this.storage = storage;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(SymbolUsageForbidden);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(ValidateNode, RoslynPresets.Identifiers);
        }

        public void ValidateNode(SyntaxNodeAnalysisContext context)
        {
            var symbol = context.SemanticModel.GetSymbolInfo(context.Node).Symbol;
            if (IsSupportedSymbol(symbol) && !storage.IsAllowed(symbol))
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    descriptor: SymbolUsageForbidden,
                    location: context.Node.GetLocation(),
                    messageArgs: symbol.ToDisplayString()));
            }
        }

        private bool IsSupportedSymbol(ISymbol symbol)
        {
            return symbol is ITypeSymbol
                || symbol is IMethodSymbol
                || symbol is IPropertySymbol
                || symbol is IEventSymbol
                || symbol is IFieldSymbol;
        }
    }
}