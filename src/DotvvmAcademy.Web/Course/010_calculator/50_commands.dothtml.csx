#load "30_controls.dothtml.csx"

using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;

Unit.SetDefault("./Calculator_30.dothtml");
Unit.SetCorrect("./Calculator_40.dothtml");
Unit.RemoveDependency("CalculatorViewModel_20.cs");
Unit.AddDependency("CalculatorViewModel_40.cs");
void ValidateButton(int index, string command)
{
    body.GetControl($"dot:Button[{index}]")
        .GetProperty("@Click")
        .RequireBinding(command, AllowedBinding.Command);
}

body.GetControls("dot:Button")
    .RequireCount(4);
ValidateButton(1, "Add()");
ValidateButton(2, "Subtract()");
ValidateButton(3, "Multiply()");
ValidateButton(4, "Divide()");