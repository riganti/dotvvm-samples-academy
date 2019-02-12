#load "20_datacontext.dothtml.csx"

using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.SetDefault("./ProfileDetail_30.dothtml");
Unit.SetCorrect("./ProfileDetail_40.dothtml");

div.GetControl("dot:TextBox")
    .GetProperty("@Text")
    .RequireBinding("_root.NewLastName");

div.GetControl("dot:Button")
    .GetProperty("@Click")
    .RequireBinding("_root.Rename()", AllowedBinding.Command);