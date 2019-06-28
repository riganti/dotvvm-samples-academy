#load "10_collections.csharp.csx"

using System;
using System.Collections.Generic;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.GetType(typeof(void))
    .Allow();

var listType = Unit.GetType(ItemsListType)
    .Allow();
{
    listType.GetMethod("Add")
        .Allow();
    listType.GetMethod("Remove")
        .Allow();
}

viewModel.GetProperty(NewItemProperty)
    .Allow()
    .RequireType<string>()
    .RequireAccess(AllowedAccess.Public)
    .RequireGetter()
    .RequireSetter();

viewModel.GetMethod(AddMethod)
    .RequireReturnType(typeof(void));

Unit.Run(c =>
{
    var item = $"Test_{Guid.NewGuid()}";
    var vm = c.Instantiate(ViewModelName);
    vm.NewItem = item;
    vm.Add();
    if (vm.Items.Count != 1 || vm.Items[0].Text != item)
    {
        c.Report(ERR_BrokenAdd);
    }
});
