#load "00_constants.csx"

using System.Collections.Generic;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.GetType<string>()
    .Allow();

Unit.GetType(ToDoItemName)
    .Allow()
    .GetProperty("Text")
        .Allow();

var viewModel = Unit.GetType(ViewModelName);

viewModel.GetProperty(ItemsProperty)
    .RequireAccess(AllowedAccess.Public)
    .RequireGetter()
    .RequireSetter()
    .RequireType(ItemsListType)
    .Allow();

Unit.Run(context =>
{
    var vm = context.Instantiate(ViewModelName);
    if (vm.Items is null)
    {
        context.Report(ERR_ItemsNotInitialized);
    }
});
