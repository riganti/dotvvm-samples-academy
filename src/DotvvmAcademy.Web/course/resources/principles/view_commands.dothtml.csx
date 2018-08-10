#load "./view_controls.dothtml.csx"

Unit.AddSourcePath("./CalculatorViewModel_methods.cs");
Unit.SetDefaultCodePath("./calculator_controls.dothtml");
Unit.SetCorrectCodePath("./calculator_commands.dothtml");

Unit.GetControls("/html/body/dot:Button")
    .CountEquals(4);

{
    var addButton = Unit.GetControl("/html/body/dot:Button[1]");
    addButton.GetProperty("./@Click").HasBinding("Add()", BindingKind.Command);
    addButton.GetProperty("./@Text").HasValue("+");
}
{
    var subtractButton = Unit.GetControl("/html/body/dot:Button[2]");
    subtractButton.GetProperty("./@Click").HasBinding("Subtract()", BindingKind.Command);
    subtractButton.GetProperty("./@Text").HasValue("-");
}
{
    var multiplyButton = Unit.GetControl("/html/body/dot:Button[3]");
    multiplyButton.GetProperty("./@Click").HasBinding("Multiply()", BindingKind.Command);
    multiplyButton.GetProperty("./@Text").HasValue("*");
}
{
    var divideButton = Unit.GetControl("/html/body/dot:Button[4]");
    divideButton.GetProperty("./@Click").HasBinding("Divide()", BindingKind.Command);
    divideButton.GetProperty("./@Text").HasValue("/");
}