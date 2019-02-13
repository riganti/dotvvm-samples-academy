#load "20_datacontext.dothtml.csx"

using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.SetDefault("ProfileDetail_20.dothtml");
Unit.SetCorrect("ProfileDetail_30.dothtml");

div.GetControl("dot:Button[1]")
    .GetProperty("@Click")
    .RequireBinding("_root.Create()", AllowedBinding.Command);

div.GetControl("dot:Button[2]")
    .GetProperty("@Click")
    .RequireBinding("_root.Delete()", AllowedBinding.Command);