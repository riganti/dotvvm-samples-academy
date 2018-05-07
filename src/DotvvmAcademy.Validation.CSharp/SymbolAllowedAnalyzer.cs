using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SymbolAllowedAnalyzer : ValidationDiagnosticAnalyzer
    {
        public const string MetadataKey = "IsAllowed";

        public static readonly DiagnosticDescriptor SymbolUsageForbiddenDiagnostic = new DiagnosticDescriptor(
            id: "TEMP03",
            title: "Symbol Usage Forbidden",
            messageFormat: "Usage of symbol {0} is forbidden.",
            category: "Temporary",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private readonly RoslynMetadataNameProvider nameProvider;

        public SymbolAllowedAnalyzer(MetadataCollection<MetadataName> metadata, RoslynMetadataNameProvider nameProvider) : base(metadata)
        {
            this.nameProvider = nameProvider;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(SymbolUsageForbiddenDiagnostic);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(ValidateNode, RoslynConventions.Identifiers);
        }

        public void ValidateNode(SyntaxNodeAnalysisContext context)
        {
            var symbol = context.SemanticModel.GetSymbolInfo(context.Node).Symbol;
            if (!IsSupportedSymbol(symbol))
            {
                return;
            }

            var symbolName = nameProvider.GetName(symbol);
            if (!true.Equals(Metadata[symbolName, MetadataKey]))
            {
                context.ReportDiagnostic(Diagnostic.Create(SymbolUsageForbiddenDiagnostic, context.Node.GetLocation(), symbolName));
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