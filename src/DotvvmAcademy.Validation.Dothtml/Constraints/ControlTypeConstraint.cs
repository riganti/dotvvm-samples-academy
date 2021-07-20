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
            var type = converter.ToReflectionSingle(Type) as Type;
            if (type == null)
            {
                return;
            }

            var nodes = locator.Locate<IAbstractControl>(Expression);
            foreach (var control in nodes)
            {
                if (control.Metadata.Type.IsEqualTo(new ResolvedTypeDescriptor(type)))
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
