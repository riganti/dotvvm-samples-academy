using Microsoft.CodeAnalysis;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public interface ISymbolConverter
    {
        MemberInfo Convert(ISymbol symbol);
    }
}