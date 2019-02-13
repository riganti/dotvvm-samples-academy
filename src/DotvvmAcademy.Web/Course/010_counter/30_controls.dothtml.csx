#load "20_view.dothtml.csx"

using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;

Unit.SetDefault("Counter_20.dothtml");
Unit.SetCorrect("Counter_30.dothtml");

body.GetControl("dot:TextBox")
    .GetProperty("@Text")
    .RequireBinding(DifferenceName);