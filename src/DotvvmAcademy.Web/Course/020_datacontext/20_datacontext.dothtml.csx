﻿#load "00_constants.csx"

using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

public DothtmlUnit Unit { get; set; } = new DothtmlUnit();

Unit.GetDirective("/@viewModel")
    .RequireTypeArgument(ViewModelName);

var div = Unit.GetControl("/html/body/div");
{
    div.GetProperty("@DataContext")
        .RequireBinding("Profile");

    div.GetControl("dot:TextBox[1]")
        .GetProperty("@Text")
        .RequireBinding("FirstName");

    div.GetControl("dot:TextBox[2]")
        .GetProperty("@Text")
        .RequireBinding("LastName");
}