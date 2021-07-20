using System.Xml.XPath;
using DotVVM.Framework.Compilation.ControlTree;

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
            var nodes = locator.Locate<IAbstractPropertySetter>(Expression);
            foreach (var setter in nodes)
            {
                if (!(setter is IAbstractPropertyValue propertyValue))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongSetter,
                        arguments: new object[] { Resources.Setter_HardcodedValue },
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
