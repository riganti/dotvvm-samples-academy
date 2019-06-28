#load "30_add_method.csharp.csx"

using System;
using DotvvmAcademy.Validation.CSharp.Unit;

viewModel.GetMethod(RemoveMethod)
    .RequireReturnType(typeof(void))
    .RequireParameters(ToDoItemName);

Unit.Run(c =>
{
    var item = $"Test_{Guid.NewGuid()}";
    var vm = c.Instantiate(ViewModelName);
    vm.NewItem = item;
    vm.Add();
    vm.Remove(vm.Items[0]);
    if (vm.Items.Count != 0) {
        c.Report(ERR_BrokenRemove);
    }
});
