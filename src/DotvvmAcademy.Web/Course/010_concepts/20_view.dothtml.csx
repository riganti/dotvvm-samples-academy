#load "00_constants.csx"

using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;

public DothtmlUnit Unit { get; set; } = new DothtmlUnit();

Unit.GetDirective("/@viewModel")
    .RequireTypeArgument(ViewModelName);

var body = Unit.GetControl("/html/body");
{
    var div = body.GetControl("p");
    div.GetControl("dot:Literal")
        .GetProperty("@Text")
        .RequireBinding(ResultName);
}