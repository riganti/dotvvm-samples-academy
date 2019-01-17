using DotvvmAcademy.Validation.Dothtml.Constraints;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlQueryExtensions_Constraints
    {
        public static DothtmlQuery<ValidationPropertySetter> RequireBinding(
            this DothtmlQuery<ValidationPropertySetter> query,
            string value,
            AllowedBinding kind = default)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var setter in result)
                {
                    if (!(setter is ValidationPropertyBinding propertyBinding))
                    {
                        context.Report(
                            message: Resources.ERR_MandatoryBinding,
                            arguments: new object[] { setter.Property.FullName },
                            node: setter);
                    }
                    else if (propertyBinding.Binding.GetBindingKind() != kind)
                    {
                        context.Report(
                            message: Resources.ERR_WrongBindingKind,
                            arguments: new object[] { kind },
                            node: propertyBinding);
                    }
                    else if (!propertyBinding.Binding.Value.Equals(value))
                    {
                        context.Report(
                            message: Resources.ERR_WrongBindingValue,
                            arguments: new object[] { setter.Property.FullName, value },
                            node: propertyBinding.Binding);
                    }
                }
            });
        }

        public static DothtmlQuery<TResult> RequireCount<TResult>(this DothtmlQuery<TResult> query, int count)
            where TResult : ValidationTreeNode
        {
            return query.AddConstraint(new CountConstraint<TResult>(query.Expression, count));
        }

        public static DothtmlQuery<ValidationControl> RequireRawContent(
            this DothtmlQuery<ValidationControl> query,
            string expectedContent,
            bool isCaseSensitive = false)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var control in result)
                {
                    var innerTokens = control.Content.SelectMany(c => c.DothtmlNode.Tokens);
                    var actualContent = string.Concat(innerTokens.Select(t => t.Text))
                        .Trim();
                    var comparison = isCaseSensitive
                        ? StringComparison.InvariantCulture
                        : StringComparison.InvariantCultureIgnoreCase;
                    if (!expectedContent.Equals(actualContent, comparison))
                    {
                        context.Report(
                            message: Resources.ERR_WrongRawContent,
                            arguments: new object[] { expectedContent },
                            node: control);
                    }
                }
            });
        }

        public static DothtmlQuery<ValidationControl> RequireRawText(
                                    this DothtmlQuery<ValidationControl> query,
            string expected,
            bool isCaseSensitive = false)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var control in result)
                {
                    var actualContent = string.Concat(control.DothtmlNode.Tokens.Select(t => t.Text)).Trim();
                    var comparison = isCaseSensitive
                        ? StringComparison.InvariantCulture
                        : StringComparison.InvariantCultureIgnoreCase;
                    if (!expected.Equals(actualContent, comparison))
                    {
                        context.Report(
                            message: Resources.ERR_WrongRawText,
                            arguments: new object[] { expected },
                            node: control);
                    }
                }
            });
        }

        public static DothtmlQuery<ValidationControl> RequireType<TControl>(this DothtmlQuery<ValidationControl> query)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var control in result)
                {
                    if (!control.Metadata.Type.IsEqualTo(typeof(TControl)))
                    {
                        context.Report(
                            message: Resources.ERR_WrongControlType,
                            arguments: new object[] { typeof(TControl) },
                            node: control);
                    }
                }
            });
        }

        public static DothtmlQuery<ValidationDirective> RequireTypeArgument(
                            this DothtmlQuery<ValidationDirective> query,
            string typeFullName)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var directive in result)
                {
                    if (!(directive is ValidationTypeDirective typeDirective))
                    {
                        context.Report(
                            message: Resources.ERR_MandatoryTypeDirective,
                            arguments: new object[] { directive.Name },
                            node: directive);
                    }
                    // TODO: Use Roslyn's symbols instead of mere string comparison
                    else if (typeDirective.Type?.FullName != typeFullName)
                    {
                        context.Report(
                            message: Resources.ERR_WrongTypeDirectiveArgument,
                            arguments: new object[] { typeFullName },
                            node: typeDirective);
                    }
                }
            });
        }

        public static DothtmlQuery<ValidationPropertySetter> RequireValue(
            this DothtmlQuery<ValidationPropertySetter> query,
            object value)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var setter in result)
                {
                    if (!(setter is ValidationPropertyValue propertyValue))
                    {
                        context.Report(
                            message: Resources.ERR_MandatoryHardcodedValue,
                            arguments: new object[] { setter.Property.FullName },
                            node: setter);
                    }
                    else if (!propertyValue.Value.Equals(value))
                    {
                        context.Report(
                            message: Resources.ERR_WrongHardcodedValue,
                            arguments: new object[] { value },
                            node: propertyValue);
                    }
                }
            });
        }
    }
}