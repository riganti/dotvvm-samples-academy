#load "20_datacontext.dothtml.csx"

using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

div.GetControl("dot:Button[1]")
    .GetProperty("@Click")
    .RequireBinding("_root.Create()", AllowedBinding.Command);

div.GetControl("dot:Button[2]")
    .GetProperty("@Click")
    .RequireBinding("_root.Delete()", AllowedBinding.Command);