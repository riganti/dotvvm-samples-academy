#load "00_constants.csx"

using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.GetType<int>()
    .Allow();

var viewModel = Unit.GetType(ViewModelName);
viewModel.GetProperty(ResultName)
    .Allow()
    .RequireType<int>()
    .RequireGetter()
    .RequireSetter();

viewModel.GetProperty(DifferenceName)
    .Allow()
    .RequireType<int>()
    .RequireGetter()
    .RequireSetter();