using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# interface.
    /// </summary>
    [SyntaxKind(SyntaxKind.InterfaceDeclaration)]
    public interface ICSharpInterface : ICSharpAllowsInheritance, ICSharpMemberedType, ICSharpObject
    {
    }
}