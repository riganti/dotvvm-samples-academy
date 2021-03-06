#load "00_constants.csx"

using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

public DothtmlUnit Unit { get; set; } = new DothtmlUnit();

Unit.GetDirective("/@viewModel")
    .RequireTypeArgument(ViewModelName);

var repeater = Unit.GetControl("/html/body/dot:Repeater");
{
    repeater.GetProperty("@DataSource")
        .RequireBinding(ItemsProperty);
    repeater.GetProperty("@ItemTemplate")
        .GetControl("p/dot:Literal")
            .GetProperty("@Text")
            .RequireBinding("Text");
}
