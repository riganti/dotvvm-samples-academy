namespace DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType
{
    //todo
    public enum ControlBindName
    {
        [Description(Description = "control <dot:TextBox Text =\"{value: Property}\"")]
        TextBoxText,
        [Description(Description = "control <dot:ComboBox DataSource=\"{value: Property}\"")]
        ComboBoxDataSource,
        [Description(Description = "control <dot:ComboBox SelectedValue=\"{value: Property}\"")]
        ComboBoxSelectedValue,
        [Description(Description = "control <dot:ComboBox ValueMember=\"Property\"")]
        ComboBoxValueMemberNotBind,
        [Description(Description = "control <dot:ComboBox DisplayMember=\"Property\"")]
        ComboBoxDisplayMemberNotBind,
        [Description(Description = "control <dot:RadioButton CheckedItem=\"{value: Property}\"")]
        RadioButtonCheckedItem,
        [Description(Description = "control <dot:Repeater DataSource=\"{value: Property}\"")]
        RepeaterDataSource,
        [Description(Description = "control <dot:RadioButton CheckedItem=\"{value: Role}\"")]
        CheckBoxCheckedItems,
        [Description(Description = "control Literal - {{value: Property}} in Repeater")]
        RepeaterLiteral,
        [Description(Description = "element <div class=\"{value: Property}\">")]
        DivClass,
        [Description(Description = "element <div DataContext=\"{value: Property}\"> in Repeater")]
        DivDataContext,
        [Description(Description = "control <dot:Literal Value=\"{value: Property}\"")]
        LiteralValue,
        [Description(Description = "element <div class=\"{value: Property}\"> in Repeater")]
        RepeaterDivClass,
        [Description(Description = "element <div Validator.Value=\"{value: Property}\">")]
        DivValidatorValue,
        [Description(Description = "element <div Validator.InvalidCssClass=\"Property\">")]
        DivValidatorInvalidCssClass,
        [PreserveProperty(RemoveProperty = true)]
        [Description(Description = "element <div Validator.InvalidCssClass=\"Property\">")]
        DivValidatorInvalidCssClassRemove,
        [Description(Description = "control <dot:Validator Value=\"{value: Property}\">")]
        ValidatorValue,
        [Description(Description = "control <dot:Validator ShowErrorMessageText=\"true\">")]
        ValidatorShowErrorMessageText
    }
}