#load "30_add_remove.csharp.csx"

using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using System.Threading.Tasks;
using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.Dothtml;

Unit.SetDefault("ToDoViewModel_50.cs");
Unit.SetCorrect("ToDoViewModel_60.cs");

var viewModelBase = Unit.GetType<DotvvmViewModelBase>()
    .Allow();
viewModelBase.GetMethod("Load")
    .Allow();

Unit.GetType<Task>()
    .Allow();

viewModel.RequireBaseType<DotvvmViewModelBase>();

viewModel.GetMethod("Load")
    .RequireReturnType<Task>();

Unit.Run(c =>
{
    var vm = ViewModel.Instantiate(c, ViewModelName);
    if (vm.Items != null) {
        c.Report(ItemsInitializedEarlyDiagnosticMessage);
    }
    vm.Load().Wait();
    if (vm.Items == null)
    {
        c.Report(ItemsNotInitializedDiagnosticMessage);
    }
});
