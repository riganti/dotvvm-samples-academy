using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    /// <summary>
    /// A C# property accessor.
    /// </summary>
    [SyntaxKind(SyntaxKind.GetAccessorDeclaration)]
    [SyntaxKind(SyntaxKind.SetAccessorDeclaration)]
    [SyntaxKind(SyntaxKind.AddAccessorDeclaration)]
    [SyntaxKind(SyntaxKind.RemoveAccessorDeclaration)]
    [SyntaxKind(SyntaxKind.UnknownAccessorDeclaration)]
    [SymbolKind(SymbolKind.Method)]
    public interface ICSharpAccessor : ICSharpAllowsAccessModifier, ICSharpObject
    {
    }
}