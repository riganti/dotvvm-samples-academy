using System;
using System.Collections.Generic;
using System.Linq;
using DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Utils;

namespace DotvvmAcademy.Steps.Validation.Validators.CommonValidators
{
    public static class DotHtmlCommonValidator
    {
        public static ResolvedControl GetControlInRepeater<T>(ResolvedTreeRoot resolvedTreeRoot)
            where T : HtmlGenericControl
        {
            var repeaterTemplate = GetRepeaterTemplate(resolvedTreeRoot);
            return repeaterTemplate
                .GetDescendantControls<T>()
                .Single();
        }


        public static ResolvedPropertyTemplate GetRepeaterTemplate(ResolvedTreeRoot resolvedTreeRoot)
        {
            return resolvedTreeRoot.GetDescendantControls<Repeater>().Single()
                .Properties[Repeater.ItemTemplateProperty]
                .CastTo<ResolvedPropertyTemplate>();
        }


        private static void FillTextBoxTextBinding(ResolvedTreeRoot resolvedTreeRoot, ref List<string> propertyBindings)
        {
            propertyBindings.AddRange(resolvedTreeRoot.GetDescendantControls<TextBox>()
                .Select(c => c.GetValueBindingText(TextBox.TextProperty)));
        }

        private static void FillRadioButtonCheckedItemBinding(ResolvedTreeRoot resolvedTreeRoot,
            ref List<string> propertyBindings)
        {
            propertyBindings.AddRange(resolvedTreeRoot.GetDescendantControls<RadioButton>()
                .Select(c => c.GetValueBindingText(RadioButton.CheckedItemProperty)));
        }

        private static void FillRepeaterDataSourceBinding(ResolvedTreeRoot resolvedTreeRoot,
            ref List<string> propertyBindings)
        {
            propertyBindings.AddRange(resolvedTreeRoot.GetDescendantControls<Repeater>()
                .Select(c => c.GetValueBindingText(ItemsControl.DataSourceProperty)));
        }

        private static void FillCheckBoxCheckedItemsBinding(ResolvedTreeRoot resolvedTreeRoot,
            ref List<string> propertyBindings)
        {
            propertyBindings.AddRange(resolvedTreeRoot.GetDescendantControls<CheckBox>()
                .Select(c => c.GetValueBindingText(CheckBox.CheckedItemsProperty)));
        }


        private static void FillLiteralValueBinding(ResolvedTreeRoot resolvedTreeRoot, ref List<string> propertyBindings)
        {
            propertyBindings.AddRange(resolvedTreeRoot.GetDescendantControls<Literal>()
                .Select(c => c.GetValueBindingText(Literal.TextProperty))
                .Where(l => l != null));
        }

        private static void FillComboBoxDataSourceBinding(ResolvedTreeRoot resolvedTreeRoot,
            ref List<string> propertiesBindings)
        {
            propertiesBindings.Add(resolvedTreeRoot.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValueBindingText(ItemsControl.DataSourceProperty))
                .FirstOrDefault());
        }

        private static void FillComboBoxSelectedValueBinding(ResolvedTreeRoot resolvedTreeRoot,
            ref List<string> propertiesBindings)
        {
            propertiesBindings.AddRange(resolvedTreeRoot.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValueBindingText(Selector.SelectedValueProperty)));
        }

        private static void FillComboBoxValueMemberValue(ResolvedTreeRoot resolvedTreeRoot,
            ref List<string> propertiesBindings)
        {
            propertiesBindings.AddRange(resolvedTreeRoot.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValue(SelectorBase.ValueMemberProperty)));
        }

        private static void FillComboBoxDisplayMemberValue(ResolvedTreeRoot resolvedTreeRoot,
            ref List<string> propertiesBindings)
        {
            propertiesBindings.AddRange(resolvedTreeRoot.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValue(SelectorBase.DisplayMemberProperty)));
        }

        private static void FillRepeaterLiteralBinding(ResolvedTreeRoot resolvedTreeRoot,
            ref List<string> propertiesBindings)
        {
            var repeaterTemplate = GetRepeaterTemplate(resolvedTreeRoot);
            propertiesBindings.AddRange(repeaterTemplate.GetDescendantControls<Literal>()
                .Select(l => l.GetValueBindingOrNull(Literal.TextProperty))
                .Where(l => l != null).Select(l => l.Binding.Value));
        }


        private static void FillDivClassValueBinding(ResolvedTreeRoot resolvedTreeRoot,
            ref List<string> propertiesBindings)
        {
            var result = new List<string>();
            var divs = resolvedTreeRoot.GetDescendantControls<HtmlGenericControl>()
                .Where(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div").ToList();

            foreach (var resolvedControl in divs)
            {
                var divClassProperties = resolvedControl.Properties
                    .Where(p => p.Value.Property.Name == "Attributes:class")
                    .Select(p => p.Value)
                    .OfType<ResolvedPropertyBinding>()
                    .ToList();
                result.AddRange(divClassProperties.Select(a => a.Binding.Value));
            }
            propertiesBindings.AddRange(result);
        }

        private static void FillDivDataContextValueBinding(ResolvedTreeRoot resolvedTreeRoot,
            ref List<string> propertiesBindings)
        {
            var divs = resolvedTreeRoot.GetDescendantControls<HtmlGenericControl>()
                .Where(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div").ToList();
            propertiesBindings.AddRange(divs.Select(
                resolvedControl => resolvedControl.GetValueBindingText(DotvvmBindableObject.DataContextProperty)));
        }

        private static void FillDivValidationValueBinding(ResolvedTreeRoot resolvedTreeRoot,
            ref List<string> propertiesBindings)
        {
            var divs = resolvedTreeRoot.GetDescendantControls<HtmlGenericControl>()
                .Where(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div").ToList();

            //todo
            IEnumerable<string> result = divs.Select(
                rs => rs.GetValueBindingTextOrNull(Validator.ValueProperty)).
                Where(rs=> rs != null);
            propertiesBindings.AddRange(result);
        }


        private static IEnumerable<string> GetRepeaterDivClassBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var repeaterTemplate = GetRepeaterTemplate(resolvedTreeRoot);
            var result = new List<string>();

            var divs = repeaterTemplate.GetDescendantControls<HtmlGenericControl>()
                .Where(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div").ToList();

            foreach (var resolvedControl in divs)
            {
                var divClassProperties = resolvedControl.Properties
                    .Where(p => p.Value.Property.Name == "Attributes:class")
                    .Select(p => p.Value)
                    .OfType<ResolvedPropertyBinding>()
                    .ToList();
                result.AddRange(divClassProperties.Select(a => a.Binding.Value));
            }
            return result;
        }


        public static void ValidatePropertiesBindings(ResolvedTreeRoot resolvedTreeRoot,
            List<Property> propertiesToValidate)
        {
            foreach (var property in propertiesToValidate)
            {
                ValidatePropertyBinding(resolvedTreeRoot, property);
            }
        }

        public static void ValidatePropertyBinding(ResolvedTreeRoot resolvedTreeRoot, Property propertyToValidate)
        {
            var propertiesBindings = new List<string>();

            switch (propertyToValidate.TargetControlBindName)
            {
                case ControlBindName.TextBoxText:
                    FillTextBoxTextBinding(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.RadioButtonCheckedItem:
                    FillRadioButtonCheckedItemBinding(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.ComboBoxDataSource:
                    FillComboBoxDataSourceBinding(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.ComboBoxSelectedValue:
                    FillComboBoxSelectedValueBinding(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.ComboBoxValueMemberNotBind:
                    FillComboBoxValueMemberValue(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.ComboBoxDisplayMemberNotBind:
                    FillComboBoxDisplayMemberValue(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.RepeaterDataSource:
                    FillRepeaterDataSourceBinding(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.RepeaterLiteral:
                    FillRepeaterLiteralBinding(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.RepeaterDivClass:
                    propertiesBindings.AddRange(GetRepeaterDivClassBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.CheckBoxCheckedItems:
                    FillCheckBoxCheckedItemsBinding(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.LiteralValue:
                    FillLiteralValueBinding(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.DivClass:
                    FillDivClassValueBinding(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.DivDataContext:
                    FillDivDataContextValueBinding(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.DivValidationValue:
                    FillDivValidationValueBinding(resolvedTreeRoot, ref propertiesBindings);
                    break;
                case ControlBindName.NotExist:
                    throw new ArgumentException($"Property {propertyToValidate.Name}, cant be validate in DotHtml.");
                default:
                    throw new ArgumentException($"Property {propertyToValidate.Name}, cant be validate in DotHtml.");
            }

            var propertyName = propertyToValidate.Name;
            if (!propertiesBindings.Contains(propertyName))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.ValueBindingError,
                    propertyName, propertyToValidate.TargetControlBindName.ToDescriptionString()));
            }
        }

        public static void CheckTypeAndCountHtmlTag(ResolvedTreeRoot resolvedTreeRoot, string htmlTag, int count)
        {
            var counter = resolvedTreeRoot
                .GetDescendantControls<HtmlGenericControl>()
                .Count(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div");
            if (counter != count)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.HtmlTagCountError, count,
                    htmlTag));
            }
        }


        public static void CheckControlTypeCount<T>(ResolvedTreeRoot resolvedTreeRoot, int count)
            where T : HtmlGenericControl
        {
            if (resolvedTreeRoot.GetDescendantControls<T>().Count() != count)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.TypeControlCountError, count,
                    typeof(T).Name));
            }
        }

        public static void CheckControlTypeCountInRepeater<T>(ResolvedTreeRoot resolvedTreeRoot, int count)
            where T : HtmlGenericControl
        {
            var repeaterTemplate = GetRepeaterTemplate(resolvedTreeRoot);
            if (repeaterTemplate.GetDescendantControls<T>().Count() != count)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.TypeControlCountError, count,
                    typeof(T).Name));
            }
        }
    }
}