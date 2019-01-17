using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class ParametersConstraint
    {
        public ParametersConstraint(NameNode node, IEnumerable<NameNode> parameters)
        {
            Node = node;
            Parameters = parameters.ToImmutableArray();
        }

        public NameNode Node { get; }

        public ImmutableArray<NameNode> Parameters { get; }

        public void Validate(IValidationReporter reporter, MetaConverter converter)
        {
            var symbols = converter.ToRoslyn(Node)
                .OfType<IMethodSymbol>();
            foreach (var method in symbols)
            {
                if (Parameters.Length != method.Parameters.Length)
                {
                    reporter.Report(
                        message: Resources.ERR_WrongParemeterCount,
                        arguments: new object[] { method, Parameters.Length },
                        symbol: method);
                    continue;
                }

                for (var i = 0; i < method.Parameters.Length; i++)
                {
                    var expectedParameter = converter.ToRoslyn(Parameters[i])
                        .OfType<ITypeSymbol>()
                        .Single();
                    if (!method.Parameters[i].Type.Equals(expectedParameter))
                    {
                        reporter.Report(
                            message: Resources.ERR_WrongParameterType,
                            arguments: new object[] { expectedParameter },
                            symbol: method.Parameters[i]);
                    }
                }
            }
        }
    }
}