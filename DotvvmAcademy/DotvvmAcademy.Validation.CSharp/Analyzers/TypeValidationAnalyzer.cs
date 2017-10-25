using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class TypeValidationAnalyzer : ValidationAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics = new ImmutableArray<DiagnosticDescriptor>
        {
            new DiagnosticDescriptor("DA1001", Localized)
        }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(ValidateTypeDeclaration, new ImmutableArray<SyntaxKind> {SyntaxKind.ClassDeclaration, SyntaxKind.StructDeclaration, SyntaxKind.EnumDeclaration, SyntaxKind.InterfaceDeclaration });
        }

        private void ValidateTypeDeclaration(SyntaxNodeAnalysisContext context)
        {
            context.ReportDiagnostic(Diagnostic.Create)
        }
    }
}
