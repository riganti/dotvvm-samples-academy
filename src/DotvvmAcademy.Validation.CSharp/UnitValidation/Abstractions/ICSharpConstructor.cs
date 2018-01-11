using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    /// <summary>
    /// A C# constructor of a class or a struct.
    /// </summary>
    [SyntaxKind(SyntaxKind.ConstructorDeclaration)]
    [SymbolKind(SymbolKind.Method)]
    public interface ICSharpConstructor : ICSharpAllowsAccessModifier, ICSharpAllowsStaticModifier, ICSharpObject
    {
    }
}