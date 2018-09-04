using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    // NOTICE: More or less reservered for future use together with IDefinitions. Currently used only in constraint extension methods.
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