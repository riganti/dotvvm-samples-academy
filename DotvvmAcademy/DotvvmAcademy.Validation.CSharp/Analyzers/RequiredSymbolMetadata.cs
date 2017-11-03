using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    public class RequiredSymbolMetadata : IValidationAnalyzerMetadata
    {
        public SyntaxKind SyntaxKind { get; set; }
    }
}