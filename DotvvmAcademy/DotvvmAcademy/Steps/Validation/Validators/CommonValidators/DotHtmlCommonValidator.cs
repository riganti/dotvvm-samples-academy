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

        public static List<string> GetPropertyBindings(ResolvedTreeRoot root)
        {
            return root.GetDescendantControls<TextBox>()
                .Select(c => c.GetValueBindingText(TextBox.TextProperty))
                .ToList();
        }
    }
}