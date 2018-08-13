using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta
{
    public interface ISymbolLocator
    {
        ImmutableArray<ISymbol> Locate(NameNode name);
    }
}