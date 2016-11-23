using System.Linq;
using DotvvmAcademy.Steps.Validation.Validators.Lesson1;
using DotvvmAcademy.Steps.Validation.Validators.Lesson2;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Utils;

namespace DotvvmAcademy.Steps.Validation.Validators.CommonValidators
{
    public static class DotHtmlCommonValidator
    {
        public static void ValidateBasicControls(ResolvedTreeRoot root)
        {
            if (root.GetDescendantControls<TextBox>().Count() != 3)
            {
                throw new CodeValidationException(Lesson1Texts.ThreeTextBoxControlsError);
            }
            if (root.GetDescendantControls<Button>().Count() != 1)
            {
                throw new CodeValidationException(Lesson1Texts.OneButtonControlError);
            }
        }

        public static void ValidateAddTaskControlBindings(ResolvedTreeRoot root)
        {
            ValidateAddTaskControls(root);

            var propertyBindings = root.GetDescendantControls<TextBox>()
                .Select(c => c.GetValueBindingText(TextBox.TextProperty))
                .ToList();
            if (!propertyBindings.Contains("AddedTaskTitle"))
            {
                throw new CodeValidationException(Lesson2Texts.TextBoxBindingError);
            }

            root.GetDescendantControls<Button>().Single()
                .ValidateCommandBindingExpression(ButtonBase.ClickProperty, "AddTask()");
        }

        public static void ValidateRepeaterControl(ResolvedTreeRoot root)
        {
            ValidateAddTaskControlBindings(root);

            if (root.GetDescendantControls<Repeater>().Count() != 1)
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterControlError);
            }

            var repeater = root.GetDescendantControls<Repeater>().Single();
            if (repeater.GetValueBindingText(ItemsControl.DataSourceProperty) != "Tasks")
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterBindingError);
            }

            IAbstractPropertySetter setter;
            if (!repeater.TryGetProperty(Repeater.ItemTemplateProperty, out setter)
                || !(setter is ResolvedPropertyTemplate))
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterTemplateMissingDivError);
            }
            var template = (ResolvedPropertyTemplate) setter;

            var div = template.GetDescendantControls<HtmlGenericControl>().ToList();
            if (div.Count(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div") != 1)
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterTemplateMissingDivError);
            }
        }

        public static void ValidateRepeaterTemplate1(ResolvedTreeRoot root)
        {
            ValidateRepeaterControl(root);

            var template = CreateTemplate(root);

            var literals = template.GetDescendantControls<Literal>()
                .Select(l => l.GetValueBindingOrNull(Literal.TextProperty))
                .Where(l => l != null)
                .ToList();

            if (literals.Count(b => b.Binding.Value == "Title") != 1)
            {
                throw new CodeValidationException(Lesson2Texts.TitleBindingNotFound);
            }

            var linkButtons = template.GetDescendantControls<LinkButton>().ToList();
            if (linkButtons.Count != 1)
            {
                throw new CodeValidationException(Lesson2Texts.LinkButtonNotFound);
            }
        }

        public static void ValidateRepeaterTemplate2(ResolvedTreeRoot root)
        {
            ValidateRepeaterTemplate1(root);

            var template = CreateTemplate(root);

            var linkButton = template
                .GetDescendantControls<LinkButton>()
                .Single();
            if (linkButton.GetValueBindingText(HtmlGenericControl.VisibleProperty) != "!IsCompleted")
            {
                throw new CodeValidationException(Lesson2Texts.InvalidVisibleBinding);
            }
        }

        public static void ValidateRepeaterTemplate3(ResolvedTreeRoot root)
        {
            ValidateRepeaterTemplate2(root);

            var template = CreateTemplate(root);

            var linkButton = template
                .GetDescendantControls<LinkButton>()
                .Single();

            linkButton.ValidateCommandBindingExpression(ButtonBase.ClickProperty, "_parent.CompleteTask(_this)");
        }

        private static ResolvedPropertyTemplate CreateTemplate(ResolvedTreeRoot root)
        {
            return root.GetDescendantControls<Repeater>().Single()
                .Properties[Repeater.ItemTemplateProperty]
                .CastTo<ResolvedPropertyTemplate>();
        }

        public static void ValidateTextBoxBindings(ResolvedTreeRoot root)
        {
            ValidateBasicControls(root);

            var propertyBindings = root.GetDescendantControls<TextBox>()
                .Select(c => c.GetValueBindingText(TextBox.TextProperty))
                .ToList();
            //todo tttt
            if (!propertyBindings.Contains("Number1") || !propertyBindings.Contains("Number2") ||
                !propertyBindings.Contains("Result"))
            {
                throw new CodeValidationException(Lesson1Texts.TextBoxBindingsError);
            }
        }

        public static void ValidateAddTaskControls(ResolvedTreeRoot root)
        {
            if (root.GetDescendantControls<TextBox>().Count() != 1)
            {
                throw new CodeValidationException(Lesson2Texts.AddTaskTextBoxControlError);
            }
            if (root.GetDescendantControls<Button>().Count() != 1)
            {
                throw new CodeValidationException(Lesson2Texts.AddTaskButtonControlError);
            }

            var buttonTextBinding = root.GetDescendantControls<Button>()
                .Select(c => c.GetValue(ButtonBase.TextProperty))
                .SingleOrDefault();
            if (buttonTextBinding == null)
            {
                throw new CodeValidationException(Lesson2Texts.ButtonDoesNotHaveText);
            }
        }
    }
}