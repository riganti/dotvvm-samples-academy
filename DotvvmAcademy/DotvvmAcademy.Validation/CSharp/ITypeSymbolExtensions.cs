using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class ITypeSymbolExtensions
    {
        public static CSharpTypeDescriptor GetDescriptor(this ITypeSymbol symbol)
        {
            return new CSharpTypeDescriptor(symbol);
        }
    }
}