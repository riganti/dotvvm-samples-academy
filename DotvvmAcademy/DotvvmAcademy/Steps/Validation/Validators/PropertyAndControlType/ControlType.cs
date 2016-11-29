using System;
using System.Reflection;

namespace DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType
{
    //todo
    public enum ControlBindName
    {
        [Description(Description = "not exist!")] NotExist,
        [Description(Description = "control <dot:TextBox Text =\"{value: Property}\"")] TextBoxText,
        [Description(Description = "control <dot:ComboBox DataSource=\"{value: Property}\"")] ComboBoxDataSource,
        [Description(Description = "control <dot:ComboBox SelectedValue=\"{value: Property}\"")] ComboBoxSelectedValue,
        [Description(Description = "control <dot:ComboBox ValueMember=\"Property\"")] ComboBoxValueMemberNotBind,
        [Description(Description = "control <dot:ComboBox DisplayMember=\"Property\"")] ComboBoxDisplayMemberNotBind,
        [Description(Description = "control <dot:RadioButton CheckedItem=\"{value: Property}\"")] RadioButtonCheckedItem,
        [Description(Description = "control <dot:Repeater DataSource=\"{value: Property}\"")] RepeaterDataSource,
        [Description(Description = "control <dot:RadioButton CheckedItem=\"{value: Role}\"")] CheckBoxCheckedItems,
        [Description(Description = "control Literal - {{value: Property}} in Repeater")] RepeaterLiteral,
        [Description(Description = "element <div class=\"{value: Property}\">")] DivClass,
        [Description(Description = "element <div DataContext=\"{value: Property}\"> in Repeater")] DivDataContext,
        [Description(Description = "control <dot:Literal Value=\"{value: Property}\"")] LiteralValue,
        [Description(Description = "element <div class=\"{value: Property}\"> in Repeater")] RepeaterDivClass,
        [Description(Description = "element <div Validation.Value=\"{value: Property}\">")] DivValidationValue
    }

    public static class MyEnumExtensions
    {
        public static string ToDescriptionString(this ControlBindName val)
        {
            var descriptionAttribute =
                (DescriptionAttribute) typeof(ControlBindName).GetTypeInfo().GetField(val.ToString())
                    .GetCustomAttribute(typeof(DescriptionAttribute));

            return descriptionAttribute != null ? descriptionAttribute.Description : string.Empty;
        }
    }

    public class DescriptionAttribute : Attribute
    {
        public string Description { get; set; }
    }
}