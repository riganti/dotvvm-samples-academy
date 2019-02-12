#load "./20_loading_data.csharp.csx"

using System;
using System.Collections.Generic;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.SetDefault("ToDoViewModel_40.cs");
Unit.SetCorrect("ToDoViewModel_50.cs");

Unit.GetType(typeof(void))
    .Allow();

var listType = Unit.GetType<List<string>>()
    .Allow();
listType.GetMethod("Add")
    .Allow();
listType.GetMethod("Remove")
    .Allow();

viewModelType.GetProperty("NewItem")
    .RequireType<string>()
    .Allow();

viewModelType.GetMethod("Add")
    .RequireReturnType(typeof(void));

viewModelType.GetMethod("Remove")
    .RequireReturnType(typeof(void))
    .RequireParameters<string>();

Unit.Run(c =>
{
    var item = $"Add_{Guid.NewGuid()}";
    var vm = c.Instantiate(ToDoViewModel);
    vm.Items = new List<string>();
    vm.NewItem = item;
    vm.Add();
    if (vm.Items.Count != 1 || vm.Items[0] != item)
    {
        c.Report("Your Add command doesn't work properly.");
    }
});

Unit.Run(c =>
{
    var item = $"Remove_{Guid.NewGuid()}";
    var vm = c.Instantiate(ToDoViewModel);
    vm.Items = new List<string> {item};
    vm.Remove(item);
    if (vm.Items.Count != 0)
    {
        c.Report("Your Remove command doesn't work properly.");
    }
});