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
                .Select(c => c.GetValueBindingText(Repeater.DataSourceProperty))
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

        private static IEnumerable<string> GetComboBoxDataSourceBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var propertyBindings = resolvedTreeRoot.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValueBindingText(ItemsControl.DataSourceProperty))
                .ToList();
            return propertyBindings;
        }

        private static IEnumerable<string> GetComboBoxSelectedValueBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var propertyBindings = resolvedTreeRoot.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValueBindingText(Selector.SelectedValueProperty))
                .ToList();
            return propertyBindings;
        }
        private static IEnumerable<string> GetRepeaterLiteralBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var repeaterTemplate = GetRepeaterTemplate(resolvedTreeRoot);
            List<ResolvedPropertyBinding> literals = repeaterTemplate.GetDescendantControls<Literal>()
              .Select(l => l.GetValueBindingOrNull(Literal.TextProperty))
              .Where(l => l != null)
              .ToList();
            return literals.Select(l => l.Binding.Value);
        }
        private static IEnumerable<string> GetRepeaterDivClassBinding(ResolvedTreeRoot resolvedTreeRoot)
        {
            var result = new List<string>();
            var repeaterTemplate = GetRepeaterTemplate(resolvedTreeRoot);
            var resolvedControls = repeaterTemplate.GetDescendantControls<HtmlGenericControl>()
                .Where(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div")
                .ToList();

            foreach (var resolvedControl in resolvedControls)
            {
                var ac = resolvedControl.Properties
                    .Where(p => p.Value.Property.Name == "Attributes:class");
                // resolvedControl

                List<ResolvedPropertyBinding> divClassProperties = resolvedControl.Properties
               .Where(p => p.Value.Property.Name == "Attributes:class")
               .Select(p => p.Value)
               .OfType<ResolvedPropertyBinding>()
               // resolvedControl
               //.Select(c=> c.GetValueBindingText(Literal.TextProperty))
               .ToList();


            }

            //var resolvedControls = resolvedControls.Where(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div");
            



            List<ResolvedPropertyBinding> literals = repeaterTemplate.GetDescendantControls<Literal>()
              .Select(l => l.GetValueBindingOrNull(Literal.TextProperty))
              .Where(l => l != null)
              .ToList();


            return literals.Select(l => l.Binding.Value);
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
                propertiesBindings.AddRange(GetComboBoxDataSourceBinding(resolvedTreeRoot));
            }
            else if (propertyToValidate.TargetControlBindName == ControlBindName.ComboBoxSelectedValue)
            {
                propertiesBindings.AddRange(GetComboBoxSelectedValueBinding(resolvedTreeRoot));
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

            else if(propertyToValidate.TargetControlBindName == ControlBindName.CheckBoxCheckedItems)
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
    }
}