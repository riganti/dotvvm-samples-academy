#load "./30_controls.dothtml.csx"

Unit.SetDefault("./Calculator_30.dothtml");
Unit.SetCorrect("./Calculator_40.dothtml");

void ValidateButton(int index, string command)
{
    var button = body.GetControl($"dot:Button[{index}]");
    button.GetProperty("@Click").HasBinding(command, BindingKind.Command);
}

body.GetControls("dot:Button")
    .CountEquals(4);
ValidateButton(1, "Add()");
ValidateButton(2, "Subtract()");
ValidateButton(3, "Multiply()");
ValidateButton(4, "Divide()");