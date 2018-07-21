using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpQuery<TResult> : IQuery<TResult>
        where TResult : ISymbol
    {
        public CSharpQuery(string name)
        {
            Name = name;
        }

        public List<Action<CSharpConstraintContext<TResult>>> Constraints { get; }
            = new List<Action<CSharpConstraintContext<TResult>>>();

        public string Name { get; }

        public void AddConstraint(Action<CSharpConstraintContext<TResult>> constraint) => Constraints.Add(constraint);

        void IQuery<TResult>.AddConstraint(Action<IConstraintContext<TResult>> constraint) => AddConstraint(constraint);

        void IQuery.AddConstraint(Action<IConstraintContext> constraint) => AddConstraint(constraint);
    }
}