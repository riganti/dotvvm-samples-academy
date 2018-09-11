using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpQueryConstraint<TResult> : IConstraint
        where TResult : ISymbol
    {
        private readonly Action<ConstraintContext, ImmutableArray<TResult>> action;
        private readonly bool overwrite;
        private readonly CSharpQuery<TResult> query;

        public CSharpQueryConstraint(Action<ConstraintContext, ImmutableArray<TResult>> action, CSharpQuery<TResult> query, bool overwrite)
        {
            this.action = action;
            this.query = query;
            this.overwrite = overwrite;
        }

        public bool CanOverwrite(IConstraint other)
        {
            return overwrite
                && other is CSharpQueryConstraint<TResult> otherConstraint
                && action.Method.Equals(otherConstraint.action.Method)
                && query.Name.ToString().Equals(otherConstraint.query.Name.ToString());
        }

        public int GetOverwriteHashCode()
        {
            return query.Name.ToString().GetHashCode();
        }

        public void Validate(ConstraintContext context)
        {
            var result = context.Locate<TResult>(query.Name);
            action(context, result);
        }
    }
}