using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class NoAttributeConstraint
    {
        public NoAttributeConstraint(NameNode node, NameNode attributeClass)
        {
            Node = node;
            AttributeClass = attributeClass;
        }

        public NameNode AttributeClass { get; }

        public NameNode Node { get; }

        public void Validate(IValidationReporter reporter, MetaConverter converter)
        {
            var attributeClass = converter.ToRoslyn(AttributeClass)
                .OfType<INamedTypeSymbol>()
                .Single();
            var symbols = converter.ToRoslyn(Node);
            foreach (var symbol in symbols)
            {
                foreach (var attribute in symbol.GetAttributes())
                {
                    if (attribute.AttributeClass.Equals(attributeClass))
                    {
                        reporter.Report(
                            message: Resources.ERR_ForbiddenAttribute,
                            arguments: new object[] { symbol, attributeClass },
                            symbol: symbol);
                    }
                }
            }
        }
    }
}