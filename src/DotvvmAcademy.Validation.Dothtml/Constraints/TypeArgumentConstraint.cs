using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using Microsoft.CodeAnalysis;
using System.Linq;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Constraints
{
    internal class TypeArgumentConstraint
    {
        public TypeArgumentConstraint(XPathExpression expression, NameNode typeArgument)
        {
            Expression = expression;
            TypeArgument = typeArgument;
        }

        public XPathExpression Expression { get; }

        public NameNode TypeArgument { get; }

        public void Validate(IValidationReporter reporter, NodeLocator locator, MetaConverter converter)
        {
            var type = converter.ToRoslyn(TypeArgument)
                .OfType<ITypeSymbol>()
                .Single();
            var nodes = locator.Locate<ValidationDirective>(Expression);
            foreach (var directive in nodes)
            {
                if (!(directive is ValidationTypeDirective typeDirective))
                {
                    reporter.Report(
                        message: Resources.ERR_MandatoryTypeDirective,
                        arguments: new object[] { directive.Name },
                        node: directive);
                }
                else if (!typeDirective.Type.TypeSymbol.Equals(type))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongTypeDirectiveArgument,
                        arguments: new object[] { TypeArgument },
                        node: typeDirective);
                }
            }
        }
    }
}