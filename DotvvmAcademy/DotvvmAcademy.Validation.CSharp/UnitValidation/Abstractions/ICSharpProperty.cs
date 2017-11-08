using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    /// <summary>
    /// A C# property.
    /// </summary>
    [SyntaxKind(SyntaxKind.PropertyDeclaration)]
    [SymbolKind(SymbolKind.Property)]
    public interface ICSharpProperty : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsStaticModifier, ICSharpAllowsVirtualModifier, ICSharpObject, ICSharpAllowsOverrideModifier
    {
        ICSharpAccessor GetGetter();

        ICSharpAccessor GetSetter();

        CSharpTypeDescriptor Type { get; set; }

    }
}