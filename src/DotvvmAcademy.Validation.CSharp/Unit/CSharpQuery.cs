using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    // essentially a constraint builder
    public class CSharpQuery<TResult>
        where TResult : ISymbol
    {
        public CSharpQuery(CSharpUnit unit, NameNode name)
        {
            Unit = unit;
            Name = name;
        }

        public NameNode Name { get; }

        public CSharpUnit Unit { get; }
    }
}