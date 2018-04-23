using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SymbolStaticAnalyzer : ValidationDiagnosticAnalyzer
    {
        public const string MetadataKey = "IsStatic";

        public static readonly DiagnosticDescriptor SymbolMustBeStaticDiagnostic = new DiagnosticDescriptor(
            id: "TEMP05",
            title: "Symbol Must Be Static",
            messageFormat: "Symbol {0} must be declared as static.",
            category: "Temporary",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static readonly DiagnosticDescriptor SymbolMustNotBeStaticDiagnostic = new DiagnosticDescriptor(
            id: "TEMP06",
            title: "Symbol Must Not Be Static",
            messageFormat: "Symbol {0} must not be declared as static.",
            category: "Temporary",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private readonly SymbolLocator locator;
        private readonly ImmutableArray<MetadataName> names;

        public SymbolStaticAnalyzer(OldMetadataCollection metadata, SymbolLocator locator) : base(metadata)
        {
            names = metadata.GetNamesWithProperty(MetadataKey).ToImmutableArray();
            this.locator = locator;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(SymbolMustBeStaticDiagnostic, SymbolMustNotBeStaticDiagnostic);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationAction(ValidateCompilation);
        }

        private void ValidateCompilation(CompilationAnalysisContext context)
        {
            void Report(DiagnosticDescriptor descriptor, MetadataName name, ISymbol symbol)
            {
                foreach (var location in symbol.Locations)
                {
                    context.ReportDiagnostic(Diagnostic.Create(descriptor, location, name));
                }
            }

            foreach (var name in names)
            {
                var isStatic = Metadata.RequireProperty<bool>(name, MetadataKey);
                if (locator.TryLocate(name, out var symbol))
                {
                    if (isStatic && !symbol.IsStatic)
                    {
                        Report(SymbolMustBeStaticDiagnostic, name, symbol);
                    }
                    else if (!isStatic && symbol.IsStatic)
                    {
                        Report(SymbolMustNotBeStaticDiagnostic, name, symbol);
                    }
                }
            }
        }
    }
}