using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Meta
{
    public interface ISymbolNameBuilder
    {
        NameNode Build(ISymbol symbol);
    }
}