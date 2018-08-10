#load "./constants.csx"

Unit.SetCorrectCodePath("./ToDoViewModel_stub.cs");
Unit.AddSourcePath("./ToDoFacade.cs");
Unit.AddSourcePath("./ToDoItem.cs");

Unit.GetTypes(ToDoViewModel)
    .CountEquals(1)
    .IsTypeKind(TypeKind.Class)
    .HasAccessibility(Accessibility.Public);

Unit.GetField(FacadeField)
    .CountEquals(1)
    .HasAccessibility(Accessibility.Private)
    .IsReadonly()
    .IsOfType(ToDoFacade);

Unit.GetMethod(ViewModelConstructor)
    .CountEquals(1)
    .HasAccessibility(Accessibility.Public)
    .HasParameters(ToDoFacade);

Unit.GetProperties(ItemsProperty)
    .CountEquals(1)
    .HasAccessibility(Accessibility.Public)
    .HasGetter()
    .HasSetter()
    .IsOfType($"System.Collections.Generic.List`1[{ToDoItem}]");

Unit.Run(context =>
{
    var facade = context.Instantiate(ToDoFacade);
    var viewModel = context.Instantiate(ToDoViewModel, facade);
    var field = context.GetFieldValue(viewModel, "facade");
    if (field == null)
    {
        context.Report("Field 'facade' is not being set in constructor.");
    }
});