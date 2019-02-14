#load "00_constants.csx"

using System.Collections.Generic;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.SetDefault("ToDoViewModel_10.cs");
Unit.SetCorrect("ToDoViewModel_20.cs");

Unit.GetType<string>()
    .Allow();

var viewModel = Unit.GetType(ViewModelName);

viewModel.GetProperty(ItemsProperty)
    .RequireAccess(AllowedAccess.Public)
    .RequireGetter()
    .RequireSetter()
    .RequireType<List<string>>()
    .Allow();