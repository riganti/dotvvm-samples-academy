#load "./view_controls.dothtml.csx"

Unit.SetDefaultCodePath("./calculator_controls.dothtml");
Unit.SetCorrectCodePath("./calculator_commands.dothtml");
Unit.SetSourcePath("CalculatorViewModel.cs", "./CalculatorViewModel_methods.cs");

void ValidateButton(int index, string command, string text)
{
    var button = body.GetControl($"dot:Button[{index}]");
    button.GetProperty("@Click").HasBinding(command, BindingKind.Command);
    button.GetProperty("@Text").HasValue(text);
}

body.GetControls("dot:Button")
    .CountEquals(4);
ValidateButton(1, "Add()", "+");
ValidateButton(2, "Subtract()", "+");
ValidateButton(3, "Multiply()", "+");
ValidateButton(4, "Divide()", "+");