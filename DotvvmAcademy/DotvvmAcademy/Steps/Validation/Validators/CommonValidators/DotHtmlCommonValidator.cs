using System;
using System.Collections.Generic;
using System.Linq;
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


        private static IEnumerable<string> GetTextBoxTextBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var propertyBindings = resolvedTreeRoot.GetDescendantControls<TextBox>()
                .Select(c => c.GetValueBindingText(TextBox.TextProperty))
                .ToList();
            return propertyBindings;
        }

        private static IEnumerable<string> GetRadioButtonCheckedItemBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var propertyBindings = resolvedTreeRoot.GetDescendantControls<RadioButton>()
                .Select(c => c.GetValueBindingText(RadioButton.CheckedItemProperty))
                .ToList();
            return propertyBindings;
        }

        private static IEnumerable<string> GetRepeaterDataSourceBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var propertyBindings = resolvedTreeRoot.GetDescendantControls<Repeater>()
                .Select(c => c.GetValueBindingText(ItemsControl.DataSourceProperty))
                .ToList();
            return propertyBindings;
        }

        private static IEnumerable<string> GetCheckBoxCheckedItemsBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var propertyBindings = resolvedTreeRoot.GetDescendantControls<CheckBox>()
                .Select(c => c.GetValueBindingText(CheckBox.CheckedItemsProperty))
                .ToList();
            return propertyBindings;
        }


        private static IEnumerable<string> GetLiteralValueBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var propertyBindings = resolvedTreeRoot.GetDescendantControls<Literal>()
                .Select(c => c.GetValueBindingText(Literal.TextProperty))
                .Where(l => l != null)
                .ToList();
            return propertyBindings;
        }

        private static string GetComboBoxDataSourceBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            return resolvedTreeRoot.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValueBindingText(ItemsControl.DataSourceProperty))
                .FirstOrDefault();
        }

        private static string GetComboBoxSelectedValueBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            return resolvedTreeRoot.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValueBindingText(Selector.SelectedValueProperty))
                .FirstOrDefault();
        }

        private static string GetComboBoxValueMemberValue(ResolvedTreeRoot resolvedTreeRoot)
        {
            return resolvedTreeRoot.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValue(SelectorBase.ValueMemberProperty))
                .FirstOrDefault();
        }

        private static string GetComboBoxDisplayMemberValue(ResolvedTreeRoot resolvedTreeRoot)
        {
            return resolvedTreeRoot.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValue(SelectorBase.DisplayMemberProperty))
                .FirstOrDefault();
        }

        private static IEnumerable<string> GetRepeaterLiteralBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var repeaterTemplate = GetRepeaterTemplate(resolvedTreeRoot);
            var literals = repeaterTemplate.GetDescendantControls<Literal>()
                .Select(l => l.GetValueBindingOrNull(Literal.TextProperty))
                .Where(l => l != null)
                .ToList();
            return literals.Select(l => l.Binding.Value);
        }


        private static IEnumerable<string> GetDivClassValueBinding(ResolvedTreeRoot resolvedTreeRoot)
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
            return result;
        }

        private static IEnumerable<string> GetDivDataContextValueBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var divs = resolvedTreeRoot.GetDescendantControls<HtmlGenericControl>()
                .Where(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div").ToList();

            var result =
                divs.Select(
                        resolvedControl => resolvedControl.GetValueBindingText(DotvvmBindableObject.DataContextProperty))
                    .ToList();

            return result;
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
                    propertiesBindings.AddRange(GetTextBoxTextBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.RadioButtonCheckedItem:
                    propertiesBindings.AddRange(GetRadioButtonCheckedItemBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.ComboBoxDataSource:
                    propertiesBindings.Add(GetComboBoxDataSourceBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.ComboBoxSelectedValue:
                    propertiesBindings.Add(GetComboBoxSelectedValueBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.ComboBoxValueMemberNotBind:
                    propertiesBindings.Add(GetComboBoxValueMemberValue(resolvedTreeRoot));
                    break;
                case ControlBindName.ComboBoxDisplayMemberNotBind:
                    propertiesBindings.Add(GetComboBoxDisplayMemberValue(resolvedTreeRoot));
                    break;
                case ControlBindName.RepeaterDataSource:
                    propertiesBindings.AddRange(GetRepeaterDataSourceBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.RepeaterLiteral:
                    propertiesBindings.AddRange(GetRepeaterLiteralBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.RepeaterDivClass:
                    propertiesBindings.AddRange(GetRepeaterDivClassBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.CheckBoxCheckedItems:
                    propertiesBindings.AddRange(GetCheckBoxCheckedItemsBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.LiteralValue:
                    propertiesBindings.AddRange(GetLiteralValueBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.DivClass:
                    propertiesBindings.AddRange(GetDivClassValueBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.DivDataContext:
                    propertiesBindings.AddRange(GetDivDataContextValueBinding(resolvedTreeRoot));
                    break;
                case ControlBindName.NotExist:
                    throw new ArgumentException($"Property {propertyToValidate.Name}, cant be validate in DotHtml.");
            }

            var propertyName = propertyToValidate.Name;

            if (!propertiesBindings.Contains(propertyName))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.ValueBindingError, propertyName));
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