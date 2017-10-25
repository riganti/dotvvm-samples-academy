using DotvvmAcademy.Validation.CSharp.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class RequiredMemberAnalyzer : ValidationAnalyzer
    {
        private List<string> foundMembers = new List<string>();

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = new ImmutableArray<DiagnosticDescriptor>
        {
            DiagnosticDescriptors.MissingMember,
            DiagnosticDescriptors.RedundantMember
        };

        public override void Initialize(AnalysisContext context)
        {
            foundMembers.Clear();
            context.RegisterSyntaxNodeAction(ValidateNode, SyntaxKindArrays.Members);
            context.RegisterCompilationAction(ValidateCompilation);
        }

        private void ValidateCompilation(CompilationAnalysisContext context)
        {
            var missingMembers = Context.Method.RequiredMembers.Keys.Except(foundMembers);
            foreach (string missingMember in missingMembers)
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.MissingMember, Location.None, missingMember));
            }
        }

        private void ValidateNode(SyntaxNodeAnalysisContext context)
        {
            var fullName = context.ContainingSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var requiredMember = Context.Method.RequiredMembers.GetValueOrDefault(fullName);
            if (requiredMember == null || !context.Node.IsKind(requiredMember.Kind))
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.RedundantMember, context.Node.GetLocation(), fullName));
            }
        }
    }
}