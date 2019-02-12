#load "00_constants.csx"

using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.SetDefault("CalculatorViewModel_10.cs");
Unit.SetCorrect("CalculatorViewModel_20.cs");

Unit.GetType<int>()
    .Allow();

var viewModelType = Unit.GetType(CalculatorViewModel);
viewModelType.GetProperty("Result")
    .Allow()
    .RequireType<int>()
    .RequireGetter()
    .RequireSetter();

viewModelType.GetProperty("Number1")
    .Allow()
    .RequireType<int>()
    .RequireGetter()
    .RequireSetter();

viewModelType.GetProperty("Number2")
    .Allow()
    .RequireType<int>()
    .RequireGetter()
    .RequireSetter();