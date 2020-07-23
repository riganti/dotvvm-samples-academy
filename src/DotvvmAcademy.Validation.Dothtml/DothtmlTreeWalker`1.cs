using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlTreeWalker<T>
    {
        public virtual ImmutableArray<T> Visit(ValidationTreeRoot root)
        {
            var builder = ImmutableArray.CreateBuilder<T>();
            builder.AddRange(DefaultVisit(root));
            if (!root.Directives.IsDefaultOrEmpty)
            {
                foreach (var directive in root.Directives)
                {
                    builder.AddRange(DefaultVisit(directive));
                }
            }

            if (!root.Content.IsDefaultOrEmpty)
            {
                foreach (var control in root.Content)
                {
                    builder.AddRange(VisitControl(control));
                }
            }

            return builder.ToImmutable();
        }

        public virtual ImmutableArray<T> DefaultVisit(ValidationTreeNode node)
        {
            return ImmutableArray.Create<T>();
        }

        private ImmutableArray<T> VisitControl(ValidationControl control)
        {
            var builder = ImmutableArray.CreateBuilder<T>();
            builder.AddRange(DefaultVisit(control));
            if (control.PropertySetters.IsDefaultOrEmpty)
            {
                return builder.ToImmutable();
            }

            foreach (var setter in control.PropertySetters)
            {
                var results = setter switch
                {
                    ValidationPropertyBinding propertyBinding => VisitPropertyBinding(propertyBinding),
                    ValidationPropertyControl propertyControl => VisitPropertyControl(propertyControl),
                    ValidationPropertyControlCollection controlCollection
                        => VisitPropertyControlCollection(controlCollection),
                    ValidationPropertyTemplate template => VisitPropertyTemplate(template),
                    ValidationPropertyValue value => DefaultVisit(value),
                    _ => throw new NotImplementedException()
                };
                builder.AddRange(results);
            }
            return builder.ToImmutable();
        }

        private ImmutableArray<T> VisitPropertyBinding(ValidationPropertyBinding propertyBinding)
        {
            return DefaultVisit(propertyBinding)
                .AddRange(DefaultVisit(propertyBinding.Binding));
        }

        private ImmutableArray<T> VisitPropertyControl(ValidationPropertyControl propertyControl)
        {
            return DefaultVisit(propertyControl)
                .AddRange(VisitControl(propertyControl.Control));
        }

        private ImmutableArray<T> VisitPropertyControlCollection(
            ValidationPropertyControlCollection controlCollection)
        {
            var builder = DefaultVisit(controlCollection).ToBuilder();
            if (controlCollection.Controls.IsDefaultOrEmpty)
            {
                return builder.ToImmutable();
            }

            foreach (var control in controlCollection.Controls)
            {
                builder.AddRange(VisitControl(control));
            }
            return builder.ToImmutable();
        }

        private ImmutableArray<T> VisitPropertyTemplate(ValidationPropertyTemplate template)
        {
            var builder = DefaultVisit(template).ToBuilder();
            if (template.Content.IsDefaultOrEmpty)
            {
                return builder.ToImmutable();
            }

            foreach (var control in template.Content)
            {
                builder.AddRange(VisitControl(control));
            }
            return builder.ToImmutable();
        }
    }
}
