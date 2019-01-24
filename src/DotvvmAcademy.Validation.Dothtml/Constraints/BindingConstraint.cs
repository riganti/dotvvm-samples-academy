using DotVVM.Framework.Binding.Expressions;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Constraints
{
    internal class BindingConstraint
    {
        public BindingConstraint(XPathExpression expression, string value, AllowedBinding allowedBinding)
        {
            Expression = expression;
            Value = value;
            AllowedBinding = allowedBinding;
        }

        public AllowedBinding AllowedBinding { get; }

        public XPathExpression Expression { get; }

        public string Value { get; }

        public void Validate(IValidationReporter reporter, NodeLocator locator)
        {
            var nodes = locator.Locate<ValidationPropertySetter>(Expression);
            foreach (var setter in nodes)
            {
                if (!(setter is ValidationPropertyBinding propertyBinding))
                {
                    reporter.Report(
                        message: Resources.ERR_MandatoryBinding,
                        arguments: new object[] { setter.Property.FullName },
                        node: GetValidatedNode(setter));
                }
                else if (!AllowedBinding.HasFlag(GetAllowedBinding(propertyBinding.Binding.BindingType)))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongBindingKind,
                        arguments: new object[] { AllowedBinding },
                        node: GetValidatedNode(setter));
                }
                else if (!propertyBinding.Binding.Value.Equals(Value))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongBindingValue,
                        arguments: new object[] { setter.Property.FullName, Value },
                        node: GetValidatedNode(setter));
                }
            }
        }

        private ValidationTreeNode GetValidatedNode(ValidationPropertySetter setter)
        {
            return setter.Property.FullName == "DotVVM.Framework.Controls.Literal.Text" ? setter.Parent : setter;
        }

        private AllowedBinding GetAllowedBinding(Type type)
        {
            if (typeof(ControlPropertyBindingExpression).IsAssignableFrom(type))
            {
                return AllowedBinding.ControlProperty;
            }
            else if (typeof(ControlCommandBindingExpression).IsAssignableFrom(type))
            {
                return AllowedBinding.ControlCommand;
            }
            else if (typeof(ValueBindingExpression).IsAssignableFrom(type))
            {
                return AllowedBinding.Value;
            }
            else if (typeof(CommandBindingExpression).IsAssignableFrom(type))
            {
                return AllowedBinding.Command;
            }
            else if (typeof(ResourceBindingExpression).IsAssignableFrom(type))
            {
                return AllowedBinding.Resource;
            }
            else if (typeof(StaticCommandBindingExpression).IsAssignableFrom(type))
            {
                return AllowedBinding.StaticCommand;
            }
            else
            {
                throw new NotSupportedException($"Binding type \"{type}\" is not supported.");
            }
        }
    }
}