using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    public delegate void Test();

    /// <summary>
    /// A C# delegate.
    /// </summary>
    [SyntaxKind(SyntaxKind.DelegateDeclaration)]
    [SymbolKind(SymbolKind.NamedType)]
    public interface ICSharpDelegate : ICSharpAllowsAccessModifier, ICSharpObject
    {
        IList<CSharpTypeDescriptor> Parameters { get; set; }

        IList<CSharpTypeDescriptor> ReturnType { get; set; }
    }
}