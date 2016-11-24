using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Utils;

namespace DotvvmAcademy.Steps.Validation.Validators.CommonValidators
{
    public static class DotHtmlCommonValidator
    {
        public static ResolvedControl GetLinkButton(ResolvedTreeRoot root)
        {
            var template = GetPropertyTemplate(root);
            return template
                .GetDescendantControls<LinkButton>()
                .Single();
        }

        public static ResolvedPropertyTemplate GetPropertyTemplate(ResolvedTreeRoot root)
        {
            return root.GetDescendantControls<Repeater>().Single()
                .Properties[Repeater.ItemTemplateProperty]
                .CastTo<ResolvedPropertyTemplate>();
        }


        private static List<string> GetPropertyBindings(ResolvedTreeRoot root)
        {
            var propertyBindings = root.GetDescendantControls<TextBox>()
                .Select(c => c.GetValueBindingText(TextBox.TextProperty))
                .ToList();

            if (propertyBindings == null)
            {
                throw new ArgumentNullException(nameof(propertyBindings));
            }
            return propertyBindings;
        }

        public static void ValidatePropertiesBindings(ResolvedTreeRoot root, List<Property> propertiesToValidate)
        {
            foreach (var property in propertiesToValidate)
            {
                ValidatePropertyBinding(root, property);
            }
        }

        public static void ValidatePropertyBinding(ResolvedTreeRoot root, Property propertyToValidate)
        {
            var propertyBindings = GetPropertyBindings(root);

            var propertyName = propertyToValidate.Name;
            if (!propertyBindings.Contains(propertyName))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.ValueBindingError, propertyName));
            }
        }


    }
}