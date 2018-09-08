using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using System;

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

            // Correct count
            if (result.Length == Count)
            {
                return;
            }

            // Incorrect positive count
            if (result.Length > 0)
            {
                foreach (var symbol in result)
                {
                    context.Report(
                        message: Resources.ERR_WrongCount,
                        arguments: new object[] { Count, symbol },
                        symbol: symbol);
                }
                return;
            }

            // Incorrect zero count with logical parent
            var parents = context.Locate(Name.GetLogicalParent());
            if (!parents.IsDefaultOrEmpty)
            {
                foreach (var parent in parents)
                {
                    context.Report(
                        message: GetErrorParentMissing(parent.GetType()),
                        arguments: new object[] { parent, Name.GetShortName() },
                        symbol: parent);
                }
                return;
            }

            // Incorrect zero count without parent
            context.Report(
                message: GetErrorMissing(),
                arguments: new object[] { Name });
        }

        private string GetErrorMissing()
        {
            var symbolType = typeof(TResult);
            if (typeof(ITypeSymbol).IsAssignableFrom(symbolType))
            {
                return Resources.ERR_MissingType;
            }

            if (typeof(IMethodSymbol).IsAssignableFrom(symbolType))
            {
                return Resources.ERR_MissingMethod;
            }

            if (typeof(IPropertySymbol).IsAssignableFrom(symbolType))
            {
                return Resources.ERR_MissingProperty;
            }

            if (typeof(IMethodSymbol).IsAssignableFrom(symbolType))
            {
                return Resources.ERR_MissingMethod;
            }

            if (typeof(IEventSymbol).IsAssignableFrom(symbolType))
            {
                return Resources.ERR_MissingEvent;
            }

            return Resources.ERR_MissingSymbol;
        }

        private string GetErrorParentMissing(Type parentType)
        {
            var symbolType = typeof(TResult);
            if (typeof(ITypeSymbol).IsAssignableFrom(symbolType)
                && typeof(INamespaceSymbol).IsAssignableFrom(parentType))
            {
                return Resources.ERR_MissingNamespaceType;
            }

            if (typeof(ITypeSymbol).IsAssignableFrom(symbolType)
                && typeof(ITypeSymbol).IsAssignableFrom(parentType))
            {
                return Resources.ERR_MissingNestedType;
            }

            if (typeof(IMethodSymbol).IsAssignableFrom(symbolType))
            {
                return Resources.ERR_MissingTypeMethod;
            }

            if (typeof(IPropertySymbol).IsAssignableFrom(symbolType))
            {
                return Resources.ERR_MissingTypeProperty;
            }

            if (typeof(IMethodSymbol).IsAssignableFrom(symbolType))
            {
                return Resources.ERR_MissingTypeMethod;
            }

            if (typeof(IEventSymbol).IsAssignableFrom(symbolType))
            {
                return Resources.ERR_MissingTypeEvent;
            }

            return Resources.ERR_MissingSymbolMember;
        }
    }
}