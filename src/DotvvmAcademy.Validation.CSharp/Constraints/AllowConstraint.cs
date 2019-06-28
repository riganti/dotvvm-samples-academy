using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class AllowConstraint<TSymbol>
        where TSymbol : ISymbol
    {
        public AllowConstraint(NameNode node)
        {
            Node = node;
        }

        public NameNode Node { get; }

        public void Validate(MetaConverter converter, AllowedSymbolStorage storage)
        {
            var symbols = converter.ToRoslyn(Node)
                .OfType<TSymbol>();
            foreach (var symbol in symbols)
            {
                storage.AllowedSymbols.Add(symbol);
            }
        }
    }
}