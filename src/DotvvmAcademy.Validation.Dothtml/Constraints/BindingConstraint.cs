using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.ControlTree;
using System;
using System.Text;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Constraints
{
    internal class BindingConstraint
    {
        public BindingConstraint(XPathExpression expression, string value, AllowedBinding allowedBinding)
        {
            Expression = expression;
            Value = value;
            BindingKind = allowedBinding;
        }

        public AllowedBinding BindingKind { get; }

        public XPathExpression Expression { get; }

        public string Value { get; }

        public void Validate(IValidationReporter reporter, NodeLocator locator)
        {
            var nodes = locator.Locate<IAbstractPropertySetter>(Expression);
            foreach (var setter in nodes)
            {
                if (!(setter is IAbstractPropertyBinding propertyBinding)
                    || !BindingKind.HasFlag(GetAllowedBinding(propertyBinding.Binding.BindingType)))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongSetter,
                        arguments: new object[] { GetSetterString() },
                        node: GetValidatedNode(setter));
                }
                else if (!propertyBinding.Binding.Value.Equals(Value))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongBindingValue,
                        arguments: new object[] { Value },
                        node: GetValidatedNode(setter));
                }
            }
        }

        private string GetSetterString()
        {
            var sb = new StringBuilder();
            foreach (Enum kind in Enum.GetValues(typeof(AllowedBinding)))
            {
                if (sb.Length > 0)
                {
                    sb.Append(Resources.Setter_Or);
                }
                if (BindingKind.HasFlag(kind))
                {
                    sb.Append(Resources.ResourceManager.GetString($"ERR_{kind}Binding"));
                }
            }
            return sb.ToString();
        }

        private IAbstractTreeNode GetValidatedNode(IAbstractPropertySetter setter)
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
