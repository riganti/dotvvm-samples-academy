#load "10_collections.csharp.csx"

using System;
using System.Collections.Generic;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.GetType(typeof(void))
    .Allow();

var listType = Unit.GetType<List<string>>()
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

viewModel.GetMethod(RemoveMethod)
    .RequireReturnType(typeof(void))
    .RequireParameters<string>();

Unit.Run(c =>
{
    var item = $"Test_{Guid.NewGuid()}";
    var vm = c.Instantiate(ViewModelName);
    vm.Items = new List<string>();
    vm.NewItem = item;
    vm.Add();
    if (vm.Items.Count != 1 || vm.Items[0] != item)
    {
        c.Report(BrokenAddDiagnosticMessage);
    }
    vm.Remove(item);
    if (vm.Items.Count != 0) {
        c.Report(BrokenRemoveDiagnosticMessage);
    }
});