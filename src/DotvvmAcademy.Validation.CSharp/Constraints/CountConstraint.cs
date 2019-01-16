using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    internal class CountConstraint<TResult>
        where TResult : ISymbol
    {
        public CountConstraint(NameNode node, int count)
        {
            Node = node;
            Count = count;
        }

        public int Count { get; }

        public NameNode Node { get; }

        public void Validate(CSharpValidationReporter reporter, MetaConverter converter)
        {
            var result = converter.ToRoslyn(Node)
                .OfType<TResult>()
                .ToArray();

            if (result.Length == Count)
            {
                return;
            }

            if (result.Length > 0)
            {
                foreach (var symbol in result)
                {
                    reporter.Report(
                        message: Resources.ERR_WrongCount,
                        arguments: new object[] { Count, symbol },
                        symbol: symbol);
                }
                return;
            }
            else
            {
                var parents = converter.ToRoslyn(Node.GetLogicalParent())
                    .ToArray();
                if (parents.Length != 0)
                {
                    // report parents that lost their children
                    foreach (var parent in parents)
                    {
                        reporter.Report(
                            message: GetErrorParentMissing(parent.GetType()),
                            arguments: new object[] { parent, Node.GetShortName() },
                            symbol: parent);
                    }
                }
                else
                {
                    // report the world for losing several generations
                    reporter.Report(
                        message: GetErrorMissing(),
                        arguments: new object[] { Node });
                }
            }
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