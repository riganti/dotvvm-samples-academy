using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System;
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
            var type = converter.ToReflectionSingle(TypeArgument) as Type;
            if (type == null)
            {
                return;
            }

            var nodes = locator.Locate<IAbstractDirective>(Expression);
            foreach (var directive in nodes)
            {
                if (!(directive is IAbstractTypeSpecificationDirective typeDirective)
                    || !typeDirective.ResolvedType.IsEqualTo(new ResolvedTypeDescriptor(type)))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongTypeDirectiveArgument,
                        arguments: new object[] { TypeArgument },
                        node: directive);
                }
            }
        }
    }
}
