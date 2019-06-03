#load "20_repeater.dothtml.csx"

using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.GetControl("/html/body/dot:TextBox")
    .GetProperty("@Text")
    .RequireBinding(NewItemProperty);

Unit.GetControl("/html/body/dot:Button")
    .GetProperty("@Click")
    .RequireBinding($"{AddMethod}()", AllowedBinding.Command);
