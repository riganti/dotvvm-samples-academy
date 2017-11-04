using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    public class RequiredSymbolMetadata : IValidationAnalyzerMetadata
    {
        public ImmutableArray<SyntaxKind> PossibleKind { get; set; }
    }
}