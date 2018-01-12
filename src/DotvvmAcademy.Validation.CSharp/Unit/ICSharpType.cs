using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    [SymbolKind(SymbolKind.NamedType)]
    public interface ICSharpType : ICSharpObject
    {
        CSharpTypeDescriptor GetDescriptor();
    }
}