using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.StaticAnalysis
{
    public class RequiredSymbolMetadata : IStaticAnalysisMetadata
    {
        public ImmutableArray<SyntaxKind> PossibleKind { get; set; }
    }
}