using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AccessModifierAnalyzer : ValidationAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = new ImmutableArray<DiagnosticDescriptor>
        {
            DiagnosticDescriptors.IncorrectAccessModifier
        };

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationAction(ValidateCompilation);
        }

        private void ValidateCompilation(CompilationAnalysisContext context)
        {
        }
    }
}