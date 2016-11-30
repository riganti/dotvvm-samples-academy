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
        public static ResolvedControl GetControlInRepeater<T>(ResolvedContentNode resolvedContentNode)
            where T : HtmlGenericControl
        {
            var repeaterTemplate = GetRepeaterTemplate(resolvedContentNode);
            return repeaterTemplate
                .GetDescendantControls<T>()
                .Single();
        }


        public static ResolvedPropertyTemplate GetRepeaterTemplate(ResolvedContentNode resolvedContentNode)
        {
            return resolvedContentNode.GetDescendantControls<Repeater>().Single()
                .Properties[Repeater.ItemTemplateProperty]
                .CastTo<ResolvedPropertyTemplate>();
        }


        private static void FillTextBoxTextBinding(ResolvedContentNode resolvedContentNode, ref List<string> propertyBindings)
        {
            propertyBindings.AddRange(resolvedContentNode.GetDescendantControls<TextBox>()
                .Select(c => c.GetValueBindingText(TextBox.TextProperty)));
        }

        private static void FillRadioButtonCheckedItemBinding(ResolvedContentNode resolvedContentNode,
            ref List<string> propertyBindings)
        {
            propertyBindings.AddRange(resolvedContentNode.GetDescendantControls<RadioButton>()
                .Select(c => c.GetValueBindingText(RadioButton.CheckedItemProperty)));
        }

        private static void FillRepeaterDataSourceBinding(ResolvedContentNode resolvedContentNode,
            ref List<string> propertyBindings)
        {
            propertyBindings.AddRange(resolvedContentNode.GetDescendantControls<Repeater>()
                .Select(c => c.GetValueBindingText(ItemsControl.DataSourceProperty)));
        }

        private static void FillCheckBoxCheckedItemsBinding(ResolvedContentNode resolvedContentNode,
            ref List<string> propertyBindings)
        {
            propertyBindings.AddRange(resolvedContentNode.GetDescendantControls<CheckBox>()
                .Select(c => c.GetValueBindingText(CheckBox.CheckedItemsProperty)));
        }


        private static void FillLiteralValueBinding(ResolvedContentNode resolvedContentNode, ref List<string> propertyBindings)
        {
            propertyBindings.AddRange(resolvedContentNode.GetDescendantControls<Literal>()
                .Select(c => c.GetValueBindingText(Literal.TextProperty))
                .Where(l => l != null));
        }

        private static void FillComboBoxDataSourceBinding(ResolvedContentNode resolvedContentNode,
            ref List<string> propertiesBindings)
        {
            propertiesBindings.Add(resolvedContentNode.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValueBindingText(ItemsControl.DataSourceProperty))
                .FirstOrDefault());
        }

        private static void FillComboBoxSelectedValueBinding(ResolvedContentNode resolvedContentNode,
            ref List<string> propertiesBindings)
        {
            propertiesBindings.AddRange(resolvedContentNode.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValueBindingText(Selector.SelectedValueProperty)));
        }

        private static void FillComboBoxValueMemberValue(ResolvedContentNode resolvedContentNode,
            ref List<string> propertiesBindings)
        {
            propertiesBindings.AddRange(resolvedContentNode.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValue(SelectorBase.ValueMemberProperty)));
        }

        private static void FillComboBoxDisplayMemberValue(ResolvedContentNode resolvedContentNode,
            ref List<string> propertiesBindings)
        {
            propertiesBindings.AddRange(resolvedContentNode.GetDescendantControls<ComboBox>()
                .Select(c => c.GetValue(SelectorBase.DisplayMemberProperty)));
        }

        private static void FillRepeaterLiteralBinding(ResolvedContentNode resolvedContentNode,
            ref List<string> propertiesBindings)
        {
            var repeaterTemplate = GetRepeaterTemplate(resolvedContentNode);
            propertiesBindings.AddRange(repeaterTemplate.GetDescendantControls<Literal>()
                .Select(l => l.GetValueBindingOrNull(Literal.TextProperty))
                .Where(l => l != null).Select(l => l.Binding.Value));
        }


        private static void FillDivClassValue(ResolvedContentNode resolvedContentNode,
            ref List<string> propertiesBindings)
        {
            var result = new List<string>();
            List<ResolvedControl> divs = GetDivsInResolvedTreeRoot(resolvedContentNode);

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

        private static void FillDivDataContextValueBinding(ResolvedContentNode resolvedContentNode,
            ref List<string> propertiesBindings)
        {
            List<ResolvedControl> divs = GetDivsInResolvedTreeRoot(resolvedContentNode);
            propertiesBindings.AddRange(divs.Select(
                resolvedControl => resolvedControl.GetValueBindingText(DotvvmBindableObject.DataContextProperty)));
        }

        private static void FillDivValidatorValueBinding(ResolvedContentNode resolvedContentNode,
            ref List<string> propertiesBindings)
        {
            List<ResolvedControl> divs = GetDivsInResolvedTreeRoot(resolvedContentNode);
            //todo
            IEnumerable<string> result = divs.Select(
                rs => rs.GetValueBindingTextOrNull(Validator.ValueProperty)).
                Where(rs=> rs != null);
            propertiesBindings.AddRange(result);
        }

        private static void FillDivValidatorInvalidCssClassValue(ResolvedContentNode resolvedContentNode,
            ref List<string> propertiesBindings)
        {
            List<ResolvedControl> divs = GetDivsInResolvedTreeRoot(resolvedContentNode);

            IEnumerable<string> result = divs.Select(
                rs => rs.GetValueOrNull(Validator.InvalidCssClassProperty))
                .Where(rs => rs != null);
               
            propertiesBindings.AddRange(result);
        }


        private static List<ResolvedControl> GetDivsInResolvedTreeRoot(ResolvedContentNode resolvedContentNode)
        {
            //resolvedTreeRoot.Content.Where(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div").ToList()

            return resolvedContentNode.GetChildrenControls<HtmlGenericControl>()
                            .Where(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div").ToList();
        }

        private static IEnumerable<string> GetRepeaterDivClassBinding(ResolvedContentNode resolvedContentNode)
        {
            var repeaterTemplate = GetRepeaterTemplate(resolvedContentNode);
            var result = new List<string>();

            List<ResolvedControl> divs = GetDivsInResolvedTreeRoot(resolvedContentNode);

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


        public static void ValidatePropertiesBindings(ResolvedContentNode resolvedContentNode,
            List<Property> propertiesToValidate)
        {
            foreach (var property in propertiesToValidate)
            {
                ValidatePropertyBinding(resolvedContentNode, property);
            }
        }

        public static void ValidatePropertyBinding(ResolvedContentNode resolvedContentNode, Property propertyToValidate)
        {
            var propertiesBindings = new List<string>();

            switch (propertyToValidate.TargetControlBindName)
            {
                case ControlBindName.TextBoxText:
                    FillTextBoxTextBinding(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.RadioButtonCheckedItem:
                    FillRadioButtonCheckedItemBinding(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.ComboBoxDataSource:
                    FillComboBoxDataSourceBinding(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.ComboBoxSelectedValue:
                    FillComboBoxSelectedValueBinding(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.ComboBoxValueMemberNotBind:
                    FillComboBoxValueMemberValue(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.ComboBoxDisplayMemberNotBind:
                    FillComboBoxDisplayMemberValue(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.RepeaterDataSource:
                    FillRepeaterDataSourceBinding(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.RepeaterLiteral:
                    FillRepeaterLiteralBinding(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.RepeaterDivClass:
                    propertiesBindings.AddRange(GetRepeaterDivClassBinding(resolvedContentNode));
                    break;
                case ControlBindName.CheckBoxCheckedItems:
                    FillCheckBoxCheckedItemsBinding(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.LiteralValue:
                    FillLiteralValueBinding(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.DivClass:
                    FillDivClassValue(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.DivDataContext:
                    FillDivDataContextValueBinding(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.DivValidationValue:
                    FillDivValidatorValueBinding(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.DivValidatorInvalidCssClass:
                    FillDivValidatorInvalidCssClassValue(resolvedContentNode, ref propertiesBindings);
                    break;
                case ControlBindName.DivValidatorInvalidCssClassRemove:
                    FillDivValidatorInvalidCssClassValue(resolvedContentNode, ref propertiesBindings);
                    break;
                default:
                    throw new ArgumentException($"Property {propertyToValidate.Name}, cant be validate in DotHtml.");
            }

            var propertyName = propertyToValidate.Name;
            if (propertyToValidate.TargetControlBindName.RemovePropertyFromCode())
            {
                if (propertiesBindings.Contains(propertyName))
                {
                    throw new CodeValidationException(string.Format(ValidationErrorMessages.DeleteCodeError,
                        propertyName, propertyToValidate.TargetControlBindName.ToDescriptionString()));
                }
            }
            else
            {
                if (!propertiesBindings.Contains(propertyName))
                {
                    throw new CodeValidationException(string.Format(ValidationErrorMessages.ValueBindingError,
                        propertyName, propertyToValidate.TargetControlBindName.ToDescriptionString()));
                }
            }
        }

        public static void CheckTypeAndCountHtmlTag(ResolvedContentNode resolvedTreeRoot, string htmlTag, int count, CodeValidationException codeValidationException = null)
        {
            var counter = resolvedTreeRoot
                .GetChildrenControls<HtmlGenericControl>()
                .Count(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == htmlTag);
            if (counter != count)
            {
                if (codeValidationException == null)
                {
                    throw new CodeValidationException(string.Format(ValidationErrorMessages.HtmlTagCountError, count,
                   htmlTag));
                }
                throw codeValidationException;
            }
        }
        

        public static void CheckControlTypeCount<T>(ResolvedContentNode resolvedTreeRoot, int count)
            where T : HtmlGenericControl
        {
            if (resolvedTreeRoot.GetDescendantControls<T>().Count() != count)
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.TypeControlCountError, count,
                    typeof(T).Name));
            }
        }

        public static void CheckControlTypeCountInRepeater<T>(ResolvedContentNode resolvedTreeRoot, int count)
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