using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class QueryExtensions_Constraints
    {
        public static DothtmlQuery<TResult> AddQueryConstraint<TResult>(
            this DothtmlQuery<TResult> query,
            Action<ConstraintContext, ImmutableArray<TResult>> action,
            bool overwrite = true)
            where TResult : ValidationTreeNode
        {
            query.Unit.Constraints.Add(new DothtmlQueryConstraint<TResult>(action, query, overwrite));
            return query;
        }

        public static DothtmlQuery<TResult> Exists<TResult>(this DothtmlQuery<TResult> query)
            where TResult : ValidationTreeNode
        {
            query.Unit.AddDelegateConstraint(context =>
            {
                var result = context.Locate<TResult>(query.XPath);
                if (!result.IsEmpty)
                {
                    return;
                }

                var current = GetLogicalParent(query.XPath);
                var parents = default(ImmutableArray<ValidationTreeNode>);
                while (parents.IsDefaultOrEmpty && current != null)
                {
                    parents = context.Locate(current);
                    current = GetLogicalParent(current);
                }

                if (parents.IsDefaultOrEmpty)
                {
                    context.Provider.GetRequiredService<IValidationReporter>()
                        .Report($"Node '{query.XPath.Expression}' must exist.");
                    return;
                }

                foreach (var parent in parents)
                {
                    context.Report($"Node '{query.XPath.Expression}' must exist.", parent);
                }
            });
            return query;
        }

        public static DothtmlQuery<ValidationPropertySetter> HasBinding(
            this DothtmlQuery<ValidationPropertySetter> query,
            string value,
            BindingKind kind = default)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var setter in result)
                {
                    if (!(setter is ValidationPropertyBinding propertyBinding))
                    {
                        context.Report(
                            message: "Property is not set using a binding.",
                            node: setter);
                    }
                    else if (propertyBinding.Binding.GetBindingKind() != kind)
                    {
                        context.Report(
                            message: $"Property has to be set using '{kind}' binding.",
                            node: propertyBinding);
                    }
                    else if (!propertyBinding.Binding.Value.Equals(value))
                    {
                        context.Report(
                            message: $"Property is to be bound to '{value}'.",
                            node: propertyBinding.Binding);
                    }
                }
            });
        }

        public static DothtmlQuery<ValidationControl> HasRawContent(
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
                            message: $"Control's raw content is supposed to be '{expectedContent}'.",
                            node: control);
                    }
                }
            });
        }

        public static DothtmlQuery<ValidationControl> HasRawText(
            this DothtmlQuery<ValidationControl> query,
            string expectedContent,
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
                    if (!expectedContent.Equals(actualContent, comparison))
                    {
                        context.Report(
                            message: $"Control's raw text is supposed to be'{expectedContent}'.",
                            node: control);
                    }
                }
            });
        }

        public static DothtmlQuery<ValidationPropertySetter> HasValue(
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
                            message: "Property is not using a hard-coded value.",
                            node: setter);
                    }
                    else if (!propertyValue.Value.Equals(value))
                    {
                        context.Report(
                            message: $"Property value is supposed to be '{value}'.",
                            node: propertyValue);
                    }
                }
            });
        }

        public static DothtmlQuery<ValidationControl> IsOfType<TControl>(this DothtmlQuery<ValidationControl> query)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var control in result)
                {
                    if (!control.Metadata.Type.IsEqualTo(typeof(TControl)))
                    {
                        context.Report(
                            message: $"Control is not of type '{typeof(TControl)}'.",
                            node: control);
                    }
                }
            });
        }

        public static DothtmlQuery<ValidationDirective> IsViewModelDirective(
            this DothtmlQuery<ValidationDirective> query,
            string typeFullName)
        {
            return query.AddQueryConstraint((context, result) =>
            {
                foreach (var directive in result)
                {
                    if (!(directive is ValidationViewModelDirective viewModelDirective))
                    {
                        context.Report(
                            message: "Directive is not a @viewModel directive.",
                            node: directive);
                    }
                    else if (viewModelDirective.Type?.FullName != typeFullName)
                    {
                        context.Report(
                            message: $"@viewModel directive is not referencing '{typeFullName}'.",
                            node: viewModelDirective);
                    }
                }
            });
        }

        private static XPathExpression GetLogicalParent(XPathExpression xpath)
        {
            var lastSeparator = xpath.Expression.LastIndexOf('/');
            if (lastSeparator == -1)
            {
                return null;
            }

            var source = xpath.Expression.Substring(0, lastSeparator);
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }

            return XPathExpression.Compile(source);
        }
    }
}