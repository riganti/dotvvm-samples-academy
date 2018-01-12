using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# class.
    /// </summary>
    [SyntaxKind(SyntaxKind.ClassDeclaration)]
    public interface ICSharpClass : ICSharpConstructibleType, ICSharpAllowsInheritance, ICSharpAllowsAbstractModifier, ICSharpAllowsStaticModifier, ICSharpAllowsSealedModifier, ICSharpObject
    {
        bool HasDestructor { get; set; }
    }
}