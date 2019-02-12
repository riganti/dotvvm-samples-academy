#load "00_constants.csx"

using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;

public DothtmlUnit Unit { get; set; } = new DothtmlUnit();

Unit.SetDefault("Calculator_10.dothtml");
Unit.SetCorrect("Calculator_20.dothtml");
Unit.AddDependency("CalculatorViewModel_20.cs");

Unit.GetDirective("/@viewModel")
    .RequireTypeArgument(CalculatorViewModel);

var body = Unit.GetControl("/html/body");
{
    var div = body.GetControl("div");
    div.GetControl("dot:Literal")
        .GetProperty("@Text")
        .RequireBinding("Result");
}