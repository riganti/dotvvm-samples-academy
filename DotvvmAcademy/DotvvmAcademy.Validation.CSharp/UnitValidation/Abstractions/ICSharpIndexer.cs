using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    /// <summary>
    /// A C# indexer.
    /// </summary>
    [SyntaxKind(SyntaxKind.IndexerDeclaration)]
    [SymbolKind(SymbolKind.Property)]
    public interface ICSharpIndexer : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsVirtualModifier, ICSharpObject, ICSharpAllowsOverrideModifier
    {
        CSharpTypeDescriptor ReturnType { get; set; }

        ICSharpAccessor GetGetter();

        ICSharpAccessor GetSetter();
    }
}