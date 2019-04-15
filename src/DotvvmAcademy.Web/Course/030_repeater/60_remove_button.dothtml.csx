#load "40_add_button.dothtml.csx"

using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

repeater.GetControl("@ItemTemplate/dot:Button")
    .GetProperty("@Click")
    .RequireBinding($"_root.{RemoveMethod}(_this)", AllowedBinding.Command);