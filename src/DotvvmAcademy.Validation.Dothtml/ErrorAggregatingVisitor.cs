using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class ErrorAggregatingVisitor
    {
        public ImmutableArray<IValidationDiagnostic> Visit(ValidationTreeRoot root)
        {
            var builder = ImmutableArray.CreateBuilder<IValidationDiagnostic>();
            DefaultVisit(builder, root);
            if (!root.Directives.IsDefaultOrEmpty)
            {
                foreach (var directive in root.Directives)
                {
                    DefaultVisit(builder, directive);
                }
            }

            if (!root.Content.IsDefaultOrEmpty)
            {
                foreach (var control in root.Content)
                {
                    VisitControl(builder, control);
                }
            }

            return builder.ToImmutable();
        }

        private void DefaultVisit(ImmutableArray<IValidationDiagnostic>.Builder builder, ValidationTreeNode node)
        {
            foreach (var error in node.DothtmlNode.NodeErrors)
            {
                builder.Add(new ParserDothtmlDiagnostic(
                    message: error,
                    node: node.DothtmlNode,
                    source: node.TreeRoot.SourceCode,
                    severity: ValidationSeverity.Error));
            }
            foreach (var warning in node.DothtmlNode.NodeWarnings)
            {
                builder.Add(new ParserDothtmlDiagnostic(
                    message: warning,
                    node: node.DothtmlNode,
                    source: node.TreeRoot.SourceCode,
                    severity: ValidationSeverity.Warning));
            }
        }

        private void VisitControl(ImmutableArray<IValidationDiagnostic>.Builder builder, ValidationControl control)
        {
            DefaultVisit(builder, control);
            if (control.PropertySetters.IsDefaultOrEmpty)
            {
                return;
            }

            foreach (var setter in control.PropertySetters)
            {
                switch (setter)
                {
                    case ValidationPropertyBinding propertyBinding:
                        VisitPropertyBinding(builder, propertyBinding);
                        break;

                    case ValidationPropertyControl propertyControl:
                        VisitPropertyControl(builder, propertyControl);
                        break;

                    case ValidationPropertyControlCollection controlCollection:
                        VisitPropertyControlCollection(builder, controlCollection);
                        break;

                    case ValidationPropertyTemplate template:
                        VisitPropertyTemplate(builder, template);
                        break;

                    case ValidationPropertyValue value:
                        DefaultVisit(builder, value);
                        break;
                }
            }
        }

        private void VisitPropertyBinding(
            ImmutableArray<IValidationDiagnostic>.Builder builder,
            ValidationPropertyBinding propertyBinding)
        {
            DefaultVisit(builder, propertyBinding);
            DefaultVisit(builder, propertyBinding.Binding);
        }

        private void VisitPropertyControl(
            ImmutableArray<IValidationDiagnostic>.Builder builder,
            ValidationPropertyControl propertyControl)
        {
            DefaultVisit(builder, propertyControl);
            VisitControl(builder, propertyControl.Control);
        }

        private void VisitPropertyControlCollection(
            ImmutableArray<IValidationDiagnostic>.Builder builder,
            ValidationPropertyControlCollection controlCollection)
        {
            DefaultVisit(builder, controlCollection);
            if (controlCollection.Controls.IsDefaultOrEmpty)
            {
                return;
            }

            foreach (var control in controlCollection.Controls)
            {
                VisitControl(builder, control);
            }
        }

        private void VisitPropertyTemplate(
            ImmutableArray<IValidationDiagnostic>.Builder builder,
            ValidationPropertyTemplate template)
        {
            DefaultVisit(builder, template);
            if (template.Content.IsDefaultOrEmpty)
            {
                return;
            }

            foreach (var control in template.Content)
            {
                VisitControl(builder, control);
            }
        }
    }
}