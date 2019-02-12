#load "./00_constants.csx"

using System.Collections.Generic;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.SetDefault("ToDoViewModel_10.cs");
Unit.SetCorrect("ToDoViewModel_20.cs");

Unit.GetType<string>()
    .Allow();

var viewModelType = Unit.GetType(ToDoViewModel);

viewModelType.GetProperty("Items")
    .RequireType<List<string>>()
    .Allow();