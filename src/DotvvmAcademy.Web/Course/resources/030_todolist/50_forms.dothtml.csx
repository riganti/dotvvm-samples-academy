#load "./30_repeater.dothtml.csx"

using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.SetDefault("ToDo_20.dothtml");
Unit.SetCorrect("ToDo_30.dothtml");

Unit.GetControl("/html/body/dot:TextBox")
    .GetProperty("@Text")
    .RequireBinding("NewItem");

Unit.GetControl("/html/body/dot:Button")
    .GetProperty("@Click")
    .RequireBinding("Add()", AllowedBinding.Command);

repeater.GetControl("@ItemTemplate/dot:Button")
    .GetProperty("@Click")
    .RequireBinding("_root.Remove(_this)", AllowedBinding.Command);