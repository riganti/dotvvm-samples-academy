public const string ViewModelName = "DotvvmAcademy.Course.ToDoList.ToDoViewModel";
public const string ItemsProperty = "Items";
public const string NewItemProperty = "NewItem";
public const string AddMethod = "Add";
public const string RemoveMethod = "Remove";

public const string BrokenAddDiagnosticMessage = "The Add method doesn't add NewItem to Items.";
public const string BrokenRemoveDiagnosticMessage = "The Remove method doesn't remove 'item' from Items.";
public const string ItemsInitializedEarlyDiagnosticMessage = "Items must not be initialized when the ViewModel is created.";
public const string ItemsNotInitializedDiagnosticMessage = "Items must be initialized after Load is called.";