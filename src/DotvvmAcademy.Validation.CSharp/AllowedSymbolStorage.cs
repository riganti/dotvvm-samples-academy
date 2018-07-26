using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public class AllowedSymbolStorage
    {
        private HashSet<ISymbol> storage = new HashSet<ISymbol>(new SymbolComparer());

        public void Allow(ISymbol symbol)
        {
            storage.Add(symbol);
        }

        public bool IsAllowed(ISymbol symbol)
        {
            return storage.Contains(symbol);
        }

        // TODO: This is dumb
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