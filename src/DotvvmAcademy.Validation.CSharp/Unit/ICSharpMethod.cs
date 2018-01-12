using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{

    /// <summary>
    /// A C# method.
    /// </summary>
    [SyntaxKind(SyntaxKind.MethodDeclaration)]
    [SymbolKind(SymbolKind.Method)]
    public interface ICSharpMethod : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsAsyncModifier, ICSharpAllowsStaticModifier, ICSharpAllowsVirtualModifier, ICSharpObject, ICSharpAllowsOverrideModifier
    {
        CSharpTypeDescriptor ReturnType { get; set; }
    }
}