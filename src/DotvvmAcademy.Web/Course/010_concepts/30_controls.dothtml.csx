#load "20_view.dothtml.csx"

using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;

body.GetControl("dot:TextBox")
    .GetProperty("@Text")
    .RequireBinding(DifferenceName);