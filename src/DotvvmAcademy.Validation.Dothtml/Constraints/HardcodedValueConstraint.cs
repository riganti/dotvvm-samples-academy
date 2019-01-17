using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Constraints
{
    internal class HardcodedValueConstraint
    {
        public HardcodedValueConstraint(XPathExpression expression, object value)
        {
            Expression = expression;
            Value = value;
        }

        public XPathExpression Expression { get; }

        public object Value { get; }

        public void Validate(IValidationReporter reporter, NodeLocator locator)
        {
            var nodes = locator.Locate<ValidationPropertySetter>(Expression);
            foreach (var setter in nodes)
            {
                if (!(setter is ValidationPropertyValue propertyValue))
                {
                    reporter.Report(
                        message: Resources.ERR_MandatoryHardcodedValue,
                        arguments: new object[] { setter.Property.FullName },
                        node: setter);
                }
                else if (!propertyValue.Value.Equals(Value))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongHardcodedValue,
                        arguments: new object[] { Value },
                        node: propertyValue);
                }
            }
        }
    }
}