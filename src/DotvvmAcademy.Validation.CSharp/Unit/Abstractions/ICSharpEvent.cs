using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    /// <summary>
    /// A C# event field or property.
    /// </summary>
    [SyntaxKind(SyntaxKind.EventDeclaration)]
    [SyntaxKind(SyntaxKind.EventFieldDeclaration)]
    [SymbolKind(SymbolKind.Event)]
    public interface ICSharpEvent : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsStaticModifier, ICSharpAllowsVirtualModifier, ICSharpObject, ICSharpAllowsOverrideModifier
    {
        CSharpTypeDescriptor Type { get; set; }

        bool IsProperty { get; set; }
    }
}