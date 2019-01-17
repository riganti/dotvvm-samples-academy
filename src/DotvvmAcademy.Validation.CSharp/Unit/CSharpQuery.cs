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
            Node = name;
        }

        public NameNode Node { get; }

        public CSharpUnit Unit { get; }

        internal CSharpQuery<TResult> AddConstraint<TConstraint>(TConstraint constraint, params object[] parameters)
        {
            var queryParameters = new object[parameters.Length + 1];
            queryParameters[0] = Node;
            parameters.CopyTo(queryParameters, 1);
            Unit.AddConstraint(constraint, queryParameters);
            return this;
        }
    }
}