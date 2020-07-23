using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class DothtmlTreeWalker
    {
        public virtual void Visit(ValidationTreeRoot root)
        {
            if (!root.Directives.IsDefaultOrEmpty)
            {
                foreach (var directive in root.Directives)
                {
                    DefaultVisit(directive);
                }
            }

            if (!root.Content.IsDefaultOrEmpty)
            {
                foreach (var control in root.Content)
                {
                    VisitControl(control);
                }
            }

        }

        public virtual void DefaultVisit(ValidationTreeNode node)
        {
        }

        public virtual void VisitControl(ValidationControl control)
        {
            if (control.PropertySetters.IsDefaultOrEmpty)
            {
                return;
            }

            foreach (var setter in control.PropertySetters)
            {
                switch(setter)
                {
                    case ValidationPropertyBinding propertyBinding:
                        VisitPropertyBinding(propertyBinding);
                        break;
                    case ValidationPropertyControl propertyControl:
                        VisitPropertyControl(propertyControl);
                        break;
                    case ValidationPropertyControlCollection controlCollection:
                        VisitPropertyControlCollection(controlCollection);
                        break;
                    case ValidationPropertyTemplate template:
                        VisitPropertyTemplate(template);
                        break;
                    case ValidationPropertyValue value:
                        DefaultVisit(value);
                        break;
                    default:
                        throw new NotImplementedException();
                };
            }
        }

        public virtual void VisitPropertyBinding(ValidationPropertyBinding propertyBinding)
        {
            DefaultVisit(propertyBinding);
            DefaultVisit(propertyBinding.Binding);
        }

        public virtual void VisitPropertyControl(ValidationPropertyControl propertyControl)
        {
            DefaultVisit(propertyControl);
            VisitControl(propertyControl.Control);
        }

        public virtual void VisitPropertyControlCollection(
            ValidationPropertyControlCollection controlCollection)
        {
            if (controlCollection.Controls.IsDefaultOrEmpty)
            {
                return;
            }

            foreach (var control in controlCollection.Controls)
            {
                VisitControl(control);
            }
        }

        public virtual void VisitPropertyTemplate(ValidationPropertyTemplate template)
        {
            if (template.Content.IsDefaultOrEmpty)
            {
                return;
            }

            foreach (var control in template.Content)
            {
                VisitControl(control);
            }
        }
    }
}
