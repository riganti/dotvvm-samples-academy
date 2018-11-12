#load "./00_constants.csx"

using System.Collections.Generic;

Unit.SetFileName("ToDoViewModel.cs");
Unit.SetDefault("./ToDoViewModel_10.cs");
Unit.SetCorrect("./ToDoViewModel_20.cs");

Unit.GetType<string>()
    .Allow();

var viewModelType = Unit.GetType(ToDoViewModel);

viewModelType.GetProperty("Items")
    .IsOfType<List<string>>()
    .Allow();