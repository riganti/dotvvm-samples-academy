#load "30_controls.dothtml.csx"

using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;

void ValidateButton(int index, string command)
{
    body.GetControl($"dot:Button[{index}]")
        .GetProperty("@Click")
        .RequireBinding(command, AllowedBinding.Command);
}

body.GetControls("dot:Button")
    .RequireCount(2);
ValidateButton(1, $"{AddName}()");
ValidateButton(2, $"{SubtractName}()");