#load "./00_constants.csx"

Unit.SetFileName("Calculator.dothtml");
Unit.SetDefault("./Calculator_10.dothtml");
Unit.SetCorrect("./Calculator_20.dothtml");

Unit.GetDirective("/@viewModel")
    .IsViewModelDirective(CalculatorViewModel);

var body = Unit.GetControl("/html/body");
{
    var div = body.GetControl("div");
    div.GetControl("dot:Literal")
        .GetProperty("@Text")
        .HasBinding("Result");
}