using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    internal class AllowedSymbolStorage
    {
        public HashSet<ISymbol> AllowedSymbols { get; } = new HashSet<ISymbol>(new SymbolComparer());

        // TODO: This is dumb. Fix it.
        private class SymbolComparer : IEqualityComparer<ISymbol>
        {
            public bool Equals(ISymbol x, ISymbol y)
            {
                return x.ToDisplayString() == y.ToDisplayString();
            }

            public int GetHashCode(ISymbol obj)
            {
                return obj.ToDisplayString().GetHashCode();
            }
        }
    }
}