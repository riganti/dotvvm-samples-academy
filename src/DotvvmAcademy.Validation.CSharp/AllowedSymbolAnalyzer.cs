using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AllowedSymbolAnalyzer : DiagnosticAnalyzer
    {
#pragma warning disable RS2008
        public static readonly DiagnosticDescriptor SymbolUsageForbidden = new DiagnosticDescriptor(
            id: nameof(SymbolUsageForbidden),
            title: "Symbol Usage Forbidden",
            messageFormat: "Usage of symbol '{0}' is forbidden.",
            category: nameof(DotvvmAcademy),
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);
#pragma warning restore RS2008

        private readonly AllowedSymbolStorage storage;

        public AllowedSymbolAnalyzer(AllowedSymbolStorage storage)
        {
            this.storage = storage;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(SymbolUsageForbidden);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze
                | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.RegisterSyntaxNodeAction(ValidateNode, RoslynPresets.Identifiers);
        }

        public void ValidateNode(SyntaxNodeAnalysisContext context)
        {
            var symbol = context.SemanticModel.GetSymbolInfo(context.Node).Symbol;
            if (!IsSupportedSymbol(symbol))
            {
                return;
            }
            if (symbol is IMethodSymbol method && method.IsExtensionMethod)
            {
                symbol = method.ReducedFrom;
            }
            if (storage.AllowedSymbols.Contains(symbol) || storage.AllowedSymbols.Contains(symbol.OriginalDefinition))
            {
                return;
            }
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: SymbolUsageForbidden,
                location: context.Node.GetLocation(),
                messageArgs: symbol.ToDisplayString()));
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
