#load "./viewmodel_stub.csharp.csx"

Unit.GetProperty($"{ToDoViewModel}::NewItemText")
    .IsOfType<string>();

Unit.GetMethod($"{ToDoViewModel}::AddItem")
    .Returns<Task>()
    .HasParameters();

Unit.GetMethod($"{ToDoViewModel}::RemoveItem")
    .Returns<Task>()
    .HasParameters("System.Int32");


Unit.Run(context =>
{
    const string testItem = "Test Item";
    var facade = context.Instantiate(ToDoFacade);
    var viewModel = context.Instantiate(ToDoViewModel, facade);
    viewModel.NewItemText = testItem;
    viewModel.AddItems().Wait();
    if (viewModel.Items.Count != 4 || viewModel.Items[3].Text != testItem)
    {
        context.Report("Method 'AddItem' doesn't work properly.");
    }
});

Unit.Run(context =>
{
    var facade = context.Instantiate(ToDoFacade);
    var viewModel = context.Instantiate(ToDoViewModel, facade);
    viewModel.RemoveItem(1).Wait();
    if (viewModel.Items.Count != 2)
    {
        context.Report("Method 'RemoveItem' doesn't work properly.");
    }
});