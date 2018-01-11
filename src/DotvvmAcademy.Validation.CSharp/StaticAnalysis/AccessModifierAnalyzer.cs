using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AccessModifierAnalyzer : ValidationAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(DiagnosticDescriptors.IncorrectAccessModifier);

        public override void Initialize(AnalysisContext context)
        {
            base.Initialize(context);
            if(Metadata == null) return;

            context.RegisterSymbolAction(ValidateSymbol, SymbolKindPresets.All);
        }

        private void ValidateSymbol(SymbolAnalysisContext context)
        {
            var fullName = context.Symbol.ToDisplayString(CommonDisplayFormat);
            if (Metadata.Pairs.TryGetValue(fullName, out var value))
            {
                var castValue = (AccessModifierMetadata)value;
                if (context.Symbol.DeclaredAccessibility != castValue.Accessibility)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        descriptor: DiagnosticDescriptors.IncorrectAccessModifier,
                        location: context.Symbol.Locations.FirstOrDefault(),
                        messageArgs: new[] { fullName, castValue.Accessibility.ToString() }));
                }
            }
        }
    }
}