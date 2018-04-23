using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class TypeKindAnalyzer : ValidationDiagnosticAnalyzer
    {
        public const string MetadataKey = "TypeKind";

        public static readonly DiagnosticDescriptor WrongTypeKindDiagnostic = new DiagnosticDescriptor(
            id: "TEMP07",
            title: "Wrong Type Kind",
            messageFormat: "The kind of type {0} must be one of these: {1}.",
            category: "Temporary",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        private readonly SymbolLocator locator;
        private readonly ImmutableArray<MetadataName> names;

        public TypeKindAnalyzer(OldMetadataCollection metadata, SymbolLocator locator) : base(metadata)
        {
            names = metadata.GetNamesWithProperty(MetadataKey).ToImmutableArray();
            this.locator = locator;
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(WrongTypeKindDiagnostic);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationAction(ValidateCompilation);
        }

        private void ValidateCompilation(CompilationAnalysisContext context)
        {
            foreach (var name in names)
            {
                var desired = Metadata.RequireProperty<DesiredTypeKind>(name, MetadataKey);
                if(locator.TryLocate(name, out var symbol)
                    && symbol is ITypeSymbol typeSymbol
                    && !desired.HasRoslynTypeKind(typeSymbol.TypeKind))
                {
                    foreach(var location in typeSymbol.Locations)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(WrongTypeKindDiagnostic, location, name, desired.ToString("G")));
                    }
                }
            }
        }
    }
}