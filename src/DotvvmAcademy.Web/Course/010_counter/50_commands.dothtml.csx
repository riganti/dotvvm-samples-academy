#load "30_controls.dothtml.csx"

using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;

Unit.SetDefault("Counter_30.dothtml");
Unit.SetCorrect("Counter_40.dothtml");
Unit.RemoveDependency("CounterViewModel_20.cs");
Unit.AddDependency("CounterViewModel_40.cs");
void ValidateButton(int index, string command)
{
    body.GetControl($"dot:Button[{index}]")
        .GetProperty("@Click")
        .RequireBinding(command, AllowedBinding.Command);
}

body.GetControls("dot:Button")
    .RequireCount(2);
ValidateButton(1, $"{IncrementName}()");
ValidateButton(2, $"{DecrementName}()");