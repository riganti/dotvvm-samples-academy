using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    internal class AllowedSymbolStorage
    {
        public ImmutableHashSet<ISymbol>.Builder Builder { get; } = ImmutableHashSet.CreateBuilder<ISymbol>();
    }
}