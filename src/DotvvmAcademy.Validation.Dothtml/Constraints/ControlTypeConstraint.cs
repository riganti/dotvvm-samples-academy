using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using Microsoft.CodeAnalysis;
using System.Linq;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Constraints
{
    internal class ControlTypeConstraint
    {
        public ControlTypeConstraint(XPathExpression expression, NameNode type)
        {
            Expression = expression;
            Type = type;
        }

        public XPathExpression Expression { get; }

        public NameNode Type { get; }

        public void Validate(IValidationReporter reporter, NodeLocator locator, MetaConverter converter)
        {
            var type = converter.ToRoslyn(Type)
                .OfType<ITypeSymbol>()
                .SingleOrDefault();
            if (type == null)
            {
                return;
            }

            var nodes = locator.Locate<ValidationControl>(Expression);
            foreach (var control in nodes)
            {
                if (!SymbolEqualityComparer.Default.Equals(control.Metadata.Type.TypeSymbol, type))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongControlType,
                        arguments: new object[] { Type },
                        node: control);
                }
            }
        }
    }
}
