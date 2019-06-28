using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class FieldTypeConstraint
    {
        public FieldTypeConstraint(NameNode node, NameNode type)
        {
            Node = node;
            Type = type;
        }

        public NameNode Node { get; }

        public NameNode Type { get; }

        public void Validate(IValidationReporter reporter, MetaConverter converter)
        {
            var type = converter.ToRoslyn(Type)
                .OfType<ITypeSymbol>()
                .SingleOrDefault();
            if (type == null)
            {
                return;
            }

            var symbols = converter.ToRoslyn(Node)
                .OfType<IFieldSymbol>();
            foreach (var field in symbols)
            {
                if (!field.Type.Equals(type))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongFieldType,
                        arguments: new object[] { field, type },
                        symbol: field);
                }
            }
        }
    }
}