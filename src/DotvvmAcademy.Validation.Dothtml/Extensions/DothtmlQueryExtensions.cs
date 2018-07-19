using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public static class DothtmlQueryExtensions
    {
        public static DothtmlQuery<TResult> CountEquals<TResult>(
            this DothtmlQuery<TResult> query,
            int count)
            where TResult : ValidationTreeNode
        {
            query.AddConstraint(context =>
            {
                if (context.Result.Length != count)
                {
                    context.Report(
                        message: $"Found '{context.Result.Length}' of '{context.XPath}' " +
                            $"but expected to find '{count}'.");
                }
            });
            return query;
        }

        public static DothtmlQuery<ValidationPropertySetter> HasBinding(
            this DothtmlQuery<ValidationPropertySetter> query,
            string value,
            BindingKind kind = BindingKind.Value)
        {
            query.AddConstraint(context =>
            {
                foreach (var setter in context.Result)
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
                }
            });
            return query;
        }

        public static DothtmlQuery<ValidationControl> IsOfType<TControl>(
                    this DothtmlQuery<ValidationControl> query)
        {
            query.AddConstraint(context =>
            {
                foreach (var control in context.Result)
                {
                    if (!control.Metadata.Type.IsAssignableFrom(typeof(TControl)))
                    {
                        context.Report(
                            message: $"Control is not of type '{FullNamer.FromReflection(typeof(TControl))}'.",
                            node: control);
                    }
                }
            });
            return query;
        }

        public static DothtmlQuery<ValidationDirective> IsViewModelDirective(
            this DothtmlQuery<ValidationDirective> query,
            string typeFullName)
        {
            query.AddConstraint(context =>
            {
                foreach (var directive in context.Result)
                {
                    if (!(directive is ValidationViewModelDirective viewModelDirective))
                    {
                        context.Report(
                            message: "Directive is not a @viewModel directive.",
                            node: directive);
                    }
                    else if (viewModelDirective.Type.FullName != typeFullName)
                    {
                        context.Report(
                            message: $"@viewModel directive is not referencing '{typeFullName}'.",
                            node: viewModelDirective);
                    }
                }
            });
            return query;
        }

        public static DothtmlQuery<ValidationPropertySetter> StringEquals(
            this DothtmlQuery<ValidationPropertySetter> query,
            string value)
        {
            query.AddConstraint(context =>
            {
                foreach (var setter in context.Result)
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
            return query;
        }
    }
}