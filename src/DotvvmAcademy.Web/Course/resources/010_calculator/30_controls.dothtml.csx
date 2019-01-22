#load "./20_view.dothtml.csx"

using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;

Unit.SetDefault("Calculator_20.dothtml");
Unit.SetCorrect("Calculator_30.dothtml");
Unit.AddDependency("CalculatorViewModel_20.cs");

body.GetControl("dot:TextBox[1]")
    .GetProperty("@Text")
    .RequireBinding("Number1");

body.GetControl("dot:TextBox[2]")
    .GetProperty("@Text")
    .RequireBinding("Number2");