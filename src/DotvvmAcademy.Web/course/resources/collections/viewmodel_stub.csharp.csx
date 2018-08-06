#load "./constants.csx"

Unit.SetCorrectCodePath("./ToDoViewModel_stub.cs");
Unit.AddSource("./ToDoFacade.cs");
Unit.AddSource("./ToDoItem.cs");

Unit.GetTypes(ToDoViewModel)
    .CountEquals(1)
    .IsTypeKind(CSharpTypeKind.Class)
    .HasAccessibility(CSharpAccessibility.Public);

Unit.GetField(FacadeField)
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Private)
    .IsReadonly()
    .IsOfType(ToDoFacade);

Unit.GetMethod(ViewModelConstructor)
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public)
    .HasParameters(ToDoFacade);

Unit.GetProperties(ItemsProperty)
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public)
    .HasGetter()
    .HasSetter()
    .IsOfType($"System.Collections.Generic.List`1[{ToDoItem}]");

Unit.Run(context =>
{
    var facade = context.Instantiate(ToDoFacade);
    var viewModel = context.Instantiate(ToDoViewModel, facade);
    var field = context.GetFieldValue(viewModel, "facade");
    if (fiel == null)
    {
        context.Report("Field 'facade' is not being set in constructor.");
    }
});