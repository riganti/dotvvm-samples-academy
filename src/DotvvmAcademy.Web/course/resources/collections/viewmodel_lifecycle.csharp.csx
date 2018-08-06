#load "./viewmodel_stub.csharp.csx"

using DotVVM.Framework.ViewModel;

Unit.GetTypes(ToDoViewModel)
    .HasBaseType<DotvvmViewModelBase>();

Unit.GetMethods($"{ToDoViewModel}::PreRender")
    .Returns("System.Threading.Tasks.Task");

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