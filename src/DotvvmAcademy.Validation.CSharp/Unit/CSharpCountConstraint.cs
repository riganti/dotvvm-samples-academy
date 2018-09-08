using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpCountConstraint<TResult> : IConstraint
        where TResult : ISymbol
    {
        public CSharpCountConstraint(NameNode name, int count)
        {
            Name = name;
            Count = count;
        }

        public int Count { get; }

        public NameNode Name { get; }

        public bool CanOverwrite(IConstraint other)
        {
            if (other is CSharpCountConstraint<TResult> otherCount)
            {
                return Name.ToString() == otherCount.Name.ToString();
            }

            return false;
        }

        public int GetOverwriteHashCode()
        {
            return Name.ToString().GetHashCode();
        }

        public void Validate(ConstraintContext context)
        {
            var result = context.Locate<TResult>(Name);
            if (result.Length == Count)
            {
                return;
            }

            if (result.Length > 0)
            {
                foreach (var symbol in result)
                {
                    context.Report($"There must be '{Count}' of '{Name}'.", symbol);
                }
            }

            var current = Name.GetLogicalParent();
            ImmutableArray<ISymbol> parents = default;
            while (parents.IsDefaultOrEmpty && current != null)
            {
                parents = context.Locate(current);
                current = Name.GetLogicalParent();
            }

            if (parents.IsDefaultOrEmpty)
            {
                context.Provider.GetRequiredService<IValidationReporter>()
                    .Report($"Symbol '{Name}' must exist.");
                return;
            }

            foreach (var parent in parents)
            {
                context.Report($"Symbol '{Name}' must exist.", parent);
            }
        }
    }
}