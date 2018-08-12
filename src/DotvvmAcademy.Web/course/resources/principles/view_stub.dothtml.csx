#load "./constants.csx"

Unit.SetFileName("calculator.dothtml");
Unit.SetCorrectCodePath("./calculator_stub.dothtml");
Unit.SetSourcePath("CalculatorViewModel.cs", "./CalculatorViewModel_stub.cs");

Unit.GetDirective("/attribute::*")
    .IsViewModelDirective(CalculatorViewModel);

Unit.GetControl("/*[1]")
    .HasRawContent("<!doctype html>", false);

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