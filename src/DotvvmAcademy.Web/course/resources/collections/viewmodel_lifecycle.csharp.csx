#load "./viewmodel_stub.csharp.csx"

using DotVVM.Framework.ViewModel;

Unit.GetType<DotvvmViewModelBase>().Allow();
Unit.GetType<Task>().Allow();
Unit.GetType($"System.Collections.Generic.IReadOnlyList`1[{ToDoItem}]").Allow();
facade.GetMethod("GetToDoItems").Allow();
Unit.GetMethods("System.Linq.Enumerable::ToList").Allow();

viewModel.HasBaseType<DotvvmViewModelBase>();
{
    viewModel.GetMethod($"PreRender")
        .Returns<Task>();
}

Unit.Run(context => 
{
    var facade = context.Instantiate(ToDoFacade);
    var viewModel = context.Instantiate(ToDoViewModel);
    viewModel.PreRender().Wait();
    if (viewModel.Items == null || viewModel.Items.Count == 0)
    {
        context.Report("'Items' collection does not get loaded in 'PreRender'.");
    }
});