using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class ReadonlyConstraint
    {
        public ReadonlyConstraint(NameNode node)
        {
            Node = node;
        }

        public NameNode Node { get; }

        public void Validate(CSharpValidationReporter reporter, MetaConverter converter)
        {
            var symbols = converter.ToRoslyn(Node)
                .OfType<IFieldSymbol>();
            foreach (var field in symbols)
            {
                if (!field.IsReadOnly)
                {
                    reporter.Report(
                        message: Resources.ERR_MissingReadonly,
                        arguments: new object[] { field },
                        symbol: field);
                }
            }
        }
    }
}