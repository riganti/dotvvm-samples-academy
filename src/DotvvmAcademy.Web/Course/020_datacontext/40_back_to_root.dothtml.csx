#load "20_datacontext.dothtml.csx"

using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.GetControl("/html/body/dot:Button")
    .GetProperty("@Click")
        .RequireBinding("Load()", AllowedBinding.Command);

div.GetControl("dot:Button")
    .GetProperty("@Click")
        .RequireBinding("_root.Unload()", AllowedBinding.Command);