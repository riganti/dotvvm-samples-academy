using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class BaseTypeAnalyzer : ValidationDiagnosticAnalyzer
    {
        public const string MetadataKey = "BaseType";

        public static readonly DiagnosticDescriptor WrongBaseTypeDiagnostic = new DiagnosticDescriptor(
            id: "TEMP08",
            title: "Wrong Base Type",
            messageFormat: "The type {0} must inherit from type {1}.",
            category: "Temporary",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private readonly SymbolLocator locator;
        private readonly ImmutableArray<MetadataName> names;

        public BaseTypeAnalyzer(OldMetadataCollection metadata, SymbolLocator locator) : base(metadata)
        {
            names = metadata.GetNamesWithProperty(MetadataKey).ToImmutableArray();
            this.locator = locator;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(WrongBaseTypeDiagnostic);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationAction(ValidateCompilation);
        }

        private void ValidateCompilation(CompilationAnalysisContext context)
        {
            foreach (var name in names)
            {
                var baseType = Metadata.RequireProperty<MetadataName>(name, MetadataKey);
                if (locator.TryLocate(name, out var symbol)
                    && symbol is ITypeSymbol typeSymbol
                    && locator.TryLocate(baseType, out var baseTypeSymbol)
                    && !typeSymbol.BaseType.Equals(baseTypeSymbol))
                {
                    foreach (var location in typeSymbol.Locations)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(WrongBaseTypeDiagnostic, location, name, baseType));
                    }
                }
            }
        }
    }
}