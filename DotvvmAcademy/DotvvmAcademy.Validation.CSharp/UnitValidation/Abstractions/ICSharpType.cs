using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    [SymbolKind(SymbolKind.NamedType)]
    public interface ICSharpType : ICSharpObject
    {
        CSharpTypeDescriptor GetDescriptor();
    }
}