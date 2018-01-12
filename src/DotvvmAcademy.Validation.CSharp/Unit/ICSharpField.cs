using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    /// <summary>
    /// A C# field.
    /// </summary>
    [SyntaxKind(SyntaxKind.FieldDeclaration)]
    [SymbolKind(SymbolKind.Field)]
    public interface ICSharpField : ICSharpAllowsAccessModifier, ICSharpAllowsVolatileModifier, ICSharpAllowsConstModifier, ICSharpAllowsStaticModifier, ICSharpObject
    {
        CSharpTypeDescriptor Type { get; set; }
    }
}