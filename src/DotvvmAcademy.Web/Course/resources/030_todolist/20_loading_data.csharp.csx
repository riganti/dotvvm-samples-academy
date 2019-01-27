#load "./10_collections.csharp.csx"

using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using System.Threading.Tasks;
using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.CSharp.Unit;

Unit.SetDefault("ToDoViewModel_30.cs");
Unit.SetCorrect("ToDoViewModel_40.cs");

var viewModelBase = Unit.GetType<DotvvmViewModelBase>()
    .Allow();
viewModelBase.GetMethod("Init")
    .Allow();
viewModelBase.GetProperty("Context")
    .Allow();

var context = Unit.GetType<IDotvvmRequestContext>()
    .Allow();
context.GetProperty("IsPostBack")
    .Allow();

Unit.GetType<Task>()
    .Allow();

viewModelType.RequireBaseType<DotvvmViewModelBase>();

viewModelType.GetMethod("Init")
    .RequireReturnType<Task>();

Unit.Run(c =>
{
    var vm = c.Instantiate(ToDoViewModel);
    vm.Context = new DotvvmRequestContext();
    vm.Init().Wait();
    if (vm.Items == null)
    {
        c.Report("You must initialize Items if Context.IsPostBack is false.");
    }
});

Unit.Run(c => 
{
    var vm = c.Instantiate(ToDoViewModel);
    vm.Context = new DotvvmRequestContext { IsPostBack = true };
    vm.Init().Wait();
    if (vm.Items != null) {
        c.Report("You must not initialize Items if Context.IsPostback is true.");
    }
})