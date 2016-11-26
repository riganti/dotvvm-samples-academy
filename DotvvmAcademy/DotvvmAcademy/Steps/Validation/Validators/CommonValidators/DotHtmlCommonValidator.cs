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
            //todo
            var repeaterTemplate = GetRepeaterTemplate(resolvedTreeRoot);
            var literals = repeaterTemplate.GetDescendantControls<Literal>()
                .Select(l => l.GetValueBindingOrNull(Literal.TextProperty))
                .Where(l => l != null)
                .ToList();
            return literals.Select(l => l.Binding.Value);
        }

        private static IEnumerable<string> GetRepeaterDivClassBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var result = new List<string>();
            var repeaterTemplate = GetRepeaterTemplate(resolvedTreeRoot);
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

            if (propertyToValidate.TargetControlBindName == ControlBindName.TextBoxText)
            {
                propertiesBindings.AddRange(GetTextBoxTextBinding(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.RadioButtonCheckedItem)
            {
                propertiesBindings.AddRange(GetRadioButtonCheckedItemBinding(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.ComboBoxDataSource)
            {
                propertiesBindings.Add(GetComboBoxDataSourceBinding(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.ComboBoxSelectedValue)
            {
                propertiesBindings.Add(GetComboBoxSelectedValueBinding(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.ComboBoxValueMemberNotBind)
            {
                propertiesBindings.Add(GetComboBoxValueMemberValue(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.ComboBoxDisplayMemberNotBind)
            {
                propertiesBindings.Add(GetComboBoxDisplayMemberValue(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.RepeaterDataSource)
            {
                propertiesBindings.AddRange(GetRepeaterDataSourceBinding(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.RepeaterLiteral)
            {
                propertiesBindings.AddRange(GetRepeaterLiteralBinding(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.RepeaterDivClass)
            {
                propertiesBindings.AddRange(GetRepeaterDivClassBinding(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.CheckBoxCheckedItems)
            {
                propertiesBindings.AddRange(GetCheckBoxCheckedItemsBinding(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.LiteralValue)
            {
                propertiesBindings.AddRange(GetLiteralValueBinding(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.DivClass)
            {
                throw new NotImplementedException();
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.DivDataContext)
            {
                throw new NotImplementedException();
            }

            else if (propertyToValidate.TargetControlBindName == ControlBindName.NotExist)
            {
                throw new ArgumentException($"Property {propertyToValidate.Name}, cant be validate in DotHtml.");
            }


            var propertyName = propertyToValidate.Name;

            if (!propertiesBindings.Contains(propertyName))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.ValueBindingError, propertyName));
            }
        }


        public static void CheckTypeAndCount<T>(ResolvedTreeRoot resolvedTreeRoot, int count)
            where T : HtmlGenericControl
        {
            if (resolvedTreeRoot.GetDescendantControls<T>().Count() != count)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.TypeCountError, count,
                    typeof(T).Name));
            }
        }

        public static void CheckTypeAndCountInRepeater<T>(ResolvedTreeRoot resolvedTreeRoot, int count)
            where T : HtmlGenericControl
        {
            var repeaterTemplate = GetRepeaterTemplate(resolvedTreeRoot);
            if (repeaterTemplate.GetDescendantControls<T>().Count() != count)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.TypeCountError, count,
                    typeof(T).Name));
            }
        }
    }
}