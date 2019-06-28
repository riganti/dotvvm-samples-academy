using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class AttributeConstraint
    {
        public AttributeConstraint(NameNode node, NameNode attributeClass, object values)
        {
            Node = node;
            AttributeClass = attributeClass;
            Values = values;
        }

        public NameNode AttributeClass { get; }

        public NameNode Node { get; }

        public object Values { get; }

        public void Validate(IValidationReporter reporter, MetaConverter converter, AttributeExtractor extractor)
        {
            var attributeClass = converter.ToRoslyn(AttributeClass)
                .OfType<INamedTypeSymbol>()
                .SingleOrDefault();
            if (attributeClass == null)
            {
                return;
            }

            var symbols = converter.ToRoslyn(Node);
            foreach (var symbol in symbols)
            {
                var attributes = symbol.GetAttributes()
                    .Where(a => a.AttributeClass.Equals(attributeClass))
                    .ToArray();
                if (attributes.Length == 0)
                {
                    reporter.Report(
                        message: Resources.ERR_MissingAttribute,
                        arguments: new object[] { symbol, attributeClass },
                        symbol: symbol);
                }
                else if (attributes.Length > 1)
                {
                    reporter.Report(
                        message: Resources.ERR_MultipleAttributes,
                        arguments: new object[] { symbol, attributeClass },
                        symbol: symbol);
                }
                else if (Values != null)
                {
                    var attributeData = attributes[0];
                    var attribute = extractor.Extract(attributeData);
                    var attributeType = attribute.GetType();
                    foreach (var property in Values.GetType().GetProperties())
                    {
                        var propertyValue = property.GetValue(Values);
                        var attributeProperty = attributeType.GetProperty(property.Name);
                        if (!propertyValue.Equals(attributeProperty.GetValue(attribute)))
                        {
                            reporter.Report(
                                message: Resources.ERR_WrongAttributePropertyValue,
                                arguments: new object[] { property.Name, propertyValue },
                                symbol: symbol);
                        }
                    }
                }
            }
        }
    }
}