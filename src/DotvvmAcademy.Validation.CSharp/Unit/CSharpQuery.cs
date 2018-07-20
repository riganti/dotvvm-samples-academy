using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpQuery<TResult> : IQuery<TResult>
        where TResult : ISymbol
    {
        private readonly List<Action<CSharpConstraintContext<TResult>>> registered
            = new List<Action<CSharpConstraintContext<TResult>>>();

        public CSharpQuery(MetadataName name)
        {
            Name = name;
        }

        public MetadataName Name { get; }

        public void AddConstraint(Action<CSharpConstraintContext<TResult>> constraint) => AddConstraint(constraint);

        void IQuery<TResult>.AddConstraint(Action<IConstraintContext<TResult>> constraint) => AddConstraint(constraint);

        void IQuery.AddConstraint(Action<IConstraintContext> constraint) => AddConstraint(constraint);
    }
}