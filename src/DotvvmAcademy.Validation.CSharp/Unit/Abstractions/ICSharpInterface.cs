using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    /// <summary>
    /// A C# interface.
    /// </summary>
    [SyntaxKind(SyntaxKind.InterfaceDeclaration)]
    public interface ICSharpInterface : ICSharpAllowsInheritance, ICSharpMemberedType, ICSharpObject
    {
    }
}