using System.Collections.Generic;
using DotvvmAcademy.Steps.Validation.Validators.CommonValidators;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Controls;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson3
{
    public class Lesson3CommonValidator
    {
        public static List<Property> CreateStep2Properties()
        {
            return new List<Property>
            {
                new Property("FirstName", "string",ControlBindName.TextBoxText),
                new Property("LastName", "string",ControlBindName.TextBoxText)
            };
        }

        public static List<Property> CreateStep4Properties()
        {
            var properties = CreateStep2Properties();
            properties.Add(new Property("Role", "string", ControlBindName.RadioButtonCheckedItem));
            return properties;
        }
        public static List<Property> CreateStep6Properties()
        {
            var properties = CreateStep4Properties();
            properties.Add(new Property("Interests", "System.Collections.Generic.List<string>",ControlBindName.CheckBoxCheckedItems));
            return properties;
        }

        public static void CheckStepControls(ResolvedTreeRoot resolvedTreeRoot)
        {
            DotHtmlCommonValidator.CheckTypeAndCount<TextBox>(resolvedTreeRoot, 2);
        }
        public static void CheckStep5Controls(ResolvedTreeRoot resolvedTreeRoot)
        {
            CheckStepControls(resolvedTreeRoot);
            DotHtmlCommonValidator.CheckTypeAndCount<RadioButton>(resolvedTreeRoot, 2);
        }


        public static List<Property> CreateStep9Properties()
        {
            var properties = CreateStep4Properties();
            properties.AddRange(CreateOnlyStep9Properties());
            return properties;
        }

        public static List<Property> CreateOnlyStep9Properties()
        {
            return new List<Property>
            {
                new Property("SelectedCountryId", "int", ControlBindName.ComboBoxSelectedValue),
                new Property("Countries", "List<DotvvmAcademy.Tutorial.ViewModels.CountryInfo>",ControlBindName.RepeaterDataSource)
            };
        }

        public static void CheckStep11Controls(ResolvedTreeRoot resolvedTreeRoot)
        {
            CheckStep5Controls(resolvedTreeRoot);
            DotHtmlCommonValidator.CheckTypeAndCount<ComboBox>(resolvedTreeRoot, 1);
        }

        public static Property CreateNewCustomerProperty()
        {
            return new Property("NewCustomer", "DotvvmAcademy.Tutorial.ViewModels.CustomerInfo",ControlBindName.DivDataContext);
        }

        public static List<Property> CreateStep14Properties()
        {
            var properties = CreateStep9Properties();
            properties.Add(CreateNewCustomerProperty());
            return properties;
        }

    }
}