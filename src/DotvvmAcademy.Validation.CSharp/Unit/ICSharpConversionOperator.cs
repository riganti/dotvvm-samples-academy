using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# conversion operator that is either explicit or implicit.
    /// </summary>
    [SyntaxKind(SyntaxKind.ConversionOperatorDeclaration)]
    [SymbolKind(SymbolKind.Method)]
    public interface ICSharpConversionOperator : ICSharpAllowsAccessModifier, ICSharpObject
    {
        CSharpConversionModifier ConversionModifier { get; set; }
    }
}