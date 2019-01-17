using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class ReturnTypeConstraint
    {
        public ReturnTypeConstraint(NameNode node, NameNode returnType)
        {
            Node = node;
            ReturnType = returnType;
        }

        public NameNode Node { get; }

        public NameNode ReturnType { get; }

        public void Validate(IValidationReporter reporter, MetaConverter converter)
        {
            var type = converter.ToRoslyn(ReturnType)
                .OfType<ITypeSymbol>()
                .Single();
            var symbols = converter.ToRoslyn(Node)
                .OfType<IMethodSymbol>();
            foreach (var method in symbols)
            {
                if (!method.ReturnType.Equals(type))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongReturnType,
                        arguments: new object[] { method, type },
                        symbol: method);
                }
            }
        }
    }
}