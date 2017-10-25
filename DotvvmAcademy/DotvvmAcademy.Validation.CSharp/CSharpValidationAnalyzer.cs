using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CSharpValidationAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics = new ImmutableArray<DiagnosticDescriptor>
        {
            new DiagnosticDescriptor()
        }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxTreeAction(c =>
            {
                c.
            })


            context.RegisterSyntaxNodeAction(c =>
            {
                c.
            }, Microsoft.CodeAnalysis.);
        }
    }
}
