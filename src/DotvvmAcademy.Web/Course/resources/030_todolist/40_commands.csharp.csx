#load "./20_loading_data.csharp.csx"

Unit.SetDefault("./ToDoViewModel_40.cs");
Unit.SetCorrect("./ToDoViewModel_50.cs");

Unit.GetType("System.Void")
    .Allow();

Unit.GetType<List<string>>()
    .GetMethod("Add")
    .Allow();

viewModelType.GetProperty("NewItem")
    .IsOfType<string>()
    .Allow();

viewModelType.GetMethod("Add")
    .Returns("System.Void");

viewModelType.GetMethod("Remove")
    .Returns("System.Void")
    .HasParameters<string>();

Unit.Run(c =>
{
    var item = $"Add_{Guid.NewGuid()}";
    var vm = c.Instantiate(ToDoViewModel);
    vm.Items = new List<string>();
    vm.NewItem = item;
    vm.Add();
    if (vm.Items.Count != 1 || vm.Items[0] != item)
    {
        c.Report("Your Add command doesn't work properly.");
    }
});

Unit.Run(c =>
{
    var item = $"Remove_{Guid.NewGuid()}";
    var vm = c.Instantiate(ToDoViewModel);
    vm.Items = new List<string> {item};
    vm.Remove(item);
    if (vm.Items != 0)
    {
        c.Report("Your Remove command doesn't work properly.");
    }
});