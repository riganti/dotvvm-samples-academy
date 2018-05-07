using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DeclarationExistenceAnalyzer : ValidationDiagnosticAnalyzer
    {
        public const string MetadataKey = "DoesDeclarationExist";

        public static readonly DiagnosticDescriptor DeclarationForbiddenDiagnostic = new DiagnosticDescriptor(
            id: "TEMP01",
            title: "Declaration Forbidden",
            messageFormat: "Declaring symbol {0} is forbidden.",
            category: "Temporary",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static readonly DiagnosticDescriptor DeclarationRequiredDiagnostic = new DiagnosticDescriptor(
            id: "TEMP02",
            title: "Declaration Missing",
            messageFormat: "A declaration for symbol '{0}' is required.",
            category: "Temporary",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);
        private readonly SymbolLocator locator;
        private readonly ImmutableArray<MetadataName> names;

        public DeclarationExistenceAnalyzer(MetadataCollection<MetadataName> metadata, SymbolLocator locator) : base(metadata)
        {
            names = metadata.GetNamesWithProperty(MetadataKey).ToImmutableArray();
            this.locator = locator;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(DeclarationRequiredDiagnostic, DeclarationForbiddenDiagnostic);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationAction(ValidateCompilation);
        }

        private void ValidateCompilation(CompilationAnalysisContext context)
        {
            foreach (var name in names)
            {
                var doesDeclarationExist = Metadata.GetRequiredProperty<bool>(name, MetadataKey);
                var symbolExists = locator.TryLocate(name, out var symbol);
                if (doesDeclarationExist && !symbolExists)
                {
                    context.ReportDiagnostic(Diagnostic.Create(DeclarationRequiredDiagnostic, Location.None, name.FullName));
                }
                else if (!doesDeclarationExist && symbolExists)
                {
                    foreach (var location in symbol.Locations)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(DeclarationForbiddenDiagnostic, location, name.FullName));
                    }
                }
            }
        }
    }
}