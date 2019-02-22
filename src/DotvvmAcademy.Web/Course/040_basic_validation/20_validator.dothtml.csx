#load "00_constants.csx"

using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

public DothtmlUnit Unit { get; set; } = new DothtmlUnit();

Unit.GetDirective("/@viewModel")
    .RequireTypeArgument(ViewModelName);

void ValidateDiv(string xpath, string property)
{
    var div1 = Unit.GetControl(xpath);
    {
        div1.GetControl("dot:TextBox")
            .GetProperty("@Text")
            .RequireBinding(property);

        var validator = div1.GetControl("dot:Validator");
        {
            validator.GetProperty("@Validator.Value")
                .RequireBinding(property);

            validator.GetProperty("@Validator.ShowErrorMessageText")
                .RequireValue(true);
        }
    }
}

ValidateDiv("/html/body/div[1]", EmailProperty);
ValidateDiv("/html/body/div[2]", PasswordProperty);