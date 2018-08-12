using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class QueryExtensions_Constraints
    {
        public static IQuery<ValidationPropertySetter> HasBinding(
            this IQuery<ValidationPropertySetter> query,
            string value,
            BindingKind kind = default)
        {
            query.SetConstraint(nameof(HasBinding), context =>
            {
                foreach (var setter in context.Result)
                {
                    if (!(setter is ValidationPropertyBinding propertyBinding))
                    {
                        Report(
                            context: context,
                            message: "Property is not set using a binding.",
                            node: setter);
                    }
                    else if (propertyBinding.Binding.GetBindingKind() != kind)
                    {
                        Report(
                            context: context,
                            message: $"Property has to be set using '{kind}' binding.",
                            node: propertyBinding);
                    }
                    else if (!propertyBinding.Binding.Value.Equals(value))
                    {
                        Report(
                            context: context,
                            message: $"Property is to be bound to '{value}'.",
                            node: propertyBinding.Binding);
                    }
                }
            });
            return query;
        }

        public static IQuery<ValidationControl> HasRawContent(
            this IQuery<ValidationControl> query,
            string expectedContent,
            bool isCaseSensitive = true)
        {
            query.SetConstraint(nameof(HasRawContent), context =>
            {
                foreach (var control in context.Result)
                {
                    var actualContent = string.Concat(control.DothtmlNode.Tokens.Select(t => t.Text)).Trim();
                    var comparison = isCaseSensitive
                        ? StringComparison.InvariantCulture
                        : StringComparison.InvariantCultureIgnoreCase;
                    if (!expectedContent.Equals(actualContent, comparison))
                    {
                        Report(
                            context: context,
                            message: $"Control is supposed to contain '{expectedContent}'.",
                            node: control);
                    }
                }
            });
            return query;
        }

        public static IQuery<ValidationPropertySetter> HasValue(
            this IQuery<ValidationPropertySetter> query,
            object value)
        {
            query.SetConstraint(nameof(HasValue), context =>
            {
                foreach (var setter in context.Result)
                {
                    if (!(setter is ValidationPropertyValue propertyValue))
                    {
                        Report(
                            context: context,
                            message: "Property is not using a hard-coded value.",
                            node: setter);
                    }
                    else if (!propertyValue.Value.Equals(value))
                    {
                        Report(
                            context: context,
                            message: $"Property value is supposed to be '{value}'.",
                            node: propertyValue);
                    }
                }
            });
            return query;
        }

        public static IQuery<ValidationControl> IsOfType<TControl>(this IQuery<ValidationControl> query)
        {
            query.SetConstraint(nameof(IsOfType), context =>
            {
                foreach (var control in context.Result)
                {
                    if (!control.Metadata.Type.IsEqualTo(typeof(TControl)))
                    {
                        Report(
                            context: context,
                            message: $"Control is not of type '{typeof(TControl)}'.",
                            node: control);
                    }
                }
            });
            return query;
        }

        public static IQuery<ValidationDirective> IsViewModelDirective(
            this IQuery<ValidationDirective> query,
            string typeFullName)
        {
            query.SetConstraint(nameof(IsViewModelDirective), context =>
            {
                foreach (var directive in context.Result)
                {
                    if (!(directive is ValidationViewModelDirective viewModelDirective))
                    {
                        Report(
                            context: context,
                            message: "Directive is not a @viewModel directive.",
                            node: directive);
                    }
                    else if (viewModelDirective.Type?.FullName != typeFullName)
                    {
                        Report(
                            context: context,
                            message: $"@viewModel directive is not referencing '{typeFullName}'.",
                            node: viewModelDirective);
                    }
                }
            });
            return query;
        }

        private static void Report<TResult>(
            ConstraintContext<TResult> context,
            string message,
            ValidationTreeNode node,
            ValidationSeverity severity = default)
        {
            context.Provider.GetRequiredService<DothtmlValidationReporter>().Report(message, node, severity);
        }
    }
}