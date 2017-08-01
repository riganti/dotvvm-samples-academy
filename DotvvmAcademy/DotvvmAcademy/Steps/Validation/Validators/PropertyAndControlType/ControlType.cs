namespace DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType
{
    //todo
    public enum ControlBindName
    {
        [Description(Description = "Text property of the TextBox")]
        TextBoxText,

        [Description(Description = "DataSource property of the ComboBox")]
        ComboBoxDataSource,

        [Description(Description = "SelectedValue property of the ComboBox")]
        ComboBoxSelectedValue,

        [Description(Description = "ValueMember property of the ComboBox")]
        ComboBoxValueMemberNotBind,

        [Description(Description = "DisplayMember property of the ComboBox")]
        ComboBoxDisplayMemberNotBind,

        [Description(Description = "CheckedItem property of the RadioButton")]
        RadioButtonCheckedItem,

        [Description(Description = "DataSource property of the Repeater")]
        RepeaterDataSource,

        [Description(Description = "CheckedItem property of the CheckBox")]
        CheckBoxCheckedItems,

        [Description(Description = "binding inside Repeater")]
        RepeaterLiteral,

        [Description(Description = "CSS class of the div element")]
        DivClass,

        [Description(Description = "DataContext property of the div element")]
        DivDataContext,

        [Description(Description = "CSS class of the div element in Repeater")]
        RepeaterDivClass,

        [Description(Description = "Validator.Value property of the div element")]
        DivValidatorValue,

        [Description(Description = "Validator.InvalidCssClass of the div element")]
        DivValidatorInvalidCssClass,

        [Description(Description = "Validator.InvalidCssClass of the form element")]
        FormValidatorInvalidCssClass,

        [PreserveProperty(RemoveProperty = true)]
        [Description(Description = "Validator.InvalidCssClass of the div element")]
        DivValidatorInvalidCssClassRemove,

        [Description(Description = "Value property of the Validator")]
        ValidatorValue,

        [Description(Description = "ShowErrorMessageText property of the Validator")]
        ValidatorShowErrorMessageText
    }
}