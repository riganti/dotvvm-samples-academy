using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AccessModifierAnalyzer : ValidationAnalyzer
    {
        private ImmutableDictionary<string, AccessModifierMetadata> metadata;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(DiagnosticDescriptors.IncorrectAccessModifier);

        public override void Initialize(AnalysisContext context)
        {
            metadata = Request.StaticAnalysis.GetMetadata<AccessModifierMetadata>();
            if(metadata == null)
            {
                return;
            }

            context.RegisterSymbolAction(ValidateSymbol, SymbolKindPresets.All);
        }

        private void ValidateSymbol(SymbolAnalysisContext context)
        {
            var fullName = context.Symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            if (metadata.TryGetValue(fullName, out var value))
            {
                if (context.Symbol.DeclaredAccessibility != value.Accessibility)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        descriptor: DiagnosticDescriptors.IncorrectAccessModifier,
                        location: context.Symbol.Locations.FirstOrDefault(),
                        messageArgs: new[] { fullName, value.Accessibility.ToString() }));
                }
            }
        }
    }
}