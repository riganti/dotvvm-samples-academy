#load "./constants.csx"

Unit.SetFileName("ToDoViewModel.cs");
Unit.SetDefaultCodePath("./ToDoViewModel_stub.cs");
Unit.SetCorrectCodePath("./ToDoViewModel_ctor.cs");
Unit.SetSourcePath("ToDoFacade.cs", "./ToDoFacade.cs");
Unit.SetSourcePath("ToDoItem.cs", "./ToDoItem.cs");

var facade = Unit.GetType(ToDoFacade).Allow();
var item = Unit.GetType(ToDoItem).Allow();

var viewModel = Unit.GetType(ToDoViewModel)
    .IsTypeKind(TypeKind.Class)
    .HasAccessibility(Accessibility.Public);
{
    viewModel.GetField("facade")
        .HasAccessibility(Accessibility.Private)
        .IsReadonly()
        .IsOfType(ToDoFacade)
        .Allow();

    viewModel.GetMethod(".ctor")
        .HasAccessibility(Accessibility.Public)
        .HasParameters(ToDoFacade);

    viewModel.GetProperty("Items")
        .HasAccessibility(Accessibility.Public)
        .HasGetter()
        .HasSetter()
        .IsOfType($"System.Collections.Generic.List`1[{ToDoItem}]")
        .Allow();
}

Unit.Run(context =>
{
    var facade = context.Instantiate(ToDoFacade);
    var viewModel = context.Instantiate(ToDoViewModel, facade);
    var field = context.GetFieldValue(viewModel, "facade");
    if (field == null)
    {
        context.Report("Field 'facade' is not being set in the constructor.");
    }
});