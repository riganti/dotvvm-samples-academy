#load "./viewmodel_lifecycle.csharp.csx"

Unit.SetDefaultCodePath("./ToDoViewModel_lifecycle.cs");
Unit.SetCorrectCodePath("./ToDoViewModel_operations.cs");

Unit.GetType<string>().Allow();
Unit.GetType<int>().Allow();
facade.GetMethod("AddItem").Allow();
facade.GetMethod("RemoveItem").Allow();

{
    viewModel.GetProperty($"NewItemText")
        .IsOfType<string>()
        .Allow();

    viewModel.GetMethod($"AddItem")
        .Returns<Task>()
        .HasParameters();

    viewModel.GetMethod($"RemoveItem")
        .Returns<Task>()
        .HasParameters<int>();
}

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