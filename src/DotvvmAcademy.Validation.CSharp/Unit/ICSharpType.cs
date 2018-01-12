using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp.Unit.Abstractions
{
    [SymbolKind(SymbolKind.NamedType)]
    public interface ICSharpType : ICSharpObject
    {
        CSharpTypeDescriptor GetDescriptor();
    }
}