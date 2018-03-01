using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SymbolAccessibilityAnalyzer : ValidationDiagnosticAnalyzer
    {
        public const string MetadataKey = "Accessibility";

        public static readonly DiagnosticDescriptor WrongAccessibilityDiagnostic = new DiagnosticDescriptor(
            id: "TEMP04",
            title: "Wrong Accessibility",
            messageFormat: "Symbol {0} must have one of these accesibility levels: {1}.",
            category: "Temporary",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private readonly SymbolLocator locator;
        private readonly ImmutableArray<MetadataName> names;

        public SymbolAccessibilityAnalyzer(MetadataCollection metadata, SymbolLocator locator) : base(metadata)
        {
            names = metadata.GetNamesWithProperty(MetadataKey).ToImmutableArray();
            this.locator = locator;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(WrongAccessibilityDiagnostic);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationAction(ValidateCompilation);
        }

        private void ValidateCompilation(CompilationAnalysisContext context)
        {
            foreach(var name in names)
            {
                var desired = Metadata.RequireProperty<DesiredAccessibility>(name, MetadataKey);
                if(locator.TryLocate(name, out var symbol) && !desired.HasRoslynAccessibility(symbol.DeclaredAccessibility))
                {
                    foreach(var location in symbol.Locations)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(WrongAccessibilityDiagnostic, location, name, desired.ToString("G")));
                    }
                }
            }
        }
    }
}