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
            if (!overwrite)
            {
                return false;
            }

            if (other is CSharpQueryConstraint<TResult> otherConstraint)
            {
                return query.Name.ToString().Equals(otherConstraint.query.Name.ToString());
            }

            return false;
        }

        public int GetOverwriteHashCode()
        {
            return typeof(CSharpQueryConstraint<TResult>).GetHashCode() ^ query.Name.ToString().GetHashCode();
        }

        public void Validate(ConstraintContext context)
        {
            var result = context.Locate<TResult>(query.Name);
            action(context, result);
        }
    }
}