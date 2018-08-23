#load "./constants.csx"

Unit.SetFileName("calculator.dothtml");
Unit.SetDefaultCodePath("./calculator_stub.dothtml");
Unit.SetCorrectCodePath("./calculator_directive.dothtml");
Unit.SetSourcePath("CalculatorViewModel.cs", "./CalculatorViewModel_stub.cs");

Unit.GetDirective("/attribute::*")
    .IsViewModelDirective(CalculatorViewModel);

Unit.GetControl("/*[1]")
    .HasRawText("<!doctype html>", false);

var html = Unit.GetControl("/html");
{
    var head = html.GetControl("head");
    {
        head.GetControl("meta")
            .GetProperty("@Attributes-charset")
            .HasValue("utf-8");
    }
}
var body = html.GetControl("body");