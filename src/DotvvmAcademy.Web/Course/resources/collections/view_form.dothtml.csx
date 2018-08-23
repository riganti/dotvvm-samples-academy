#load "./view_stub.dothtml.csx"

Unit.SetDefaultCodePath("./todo_repeater.dothtml");
Unit.SetCorrectCodePath("./todo_form.dothtml");

Unit.SetSourcePath("ToDoViewModel.cs", "./ToDoViewModel_operations.cs");

{
    var removeButton = template.GetControl("dot:Button");
    removeButton.GetProperty("@Click")
        .HasBinding("_root.RemoveItem(Id)", BindingKind.Command);
}
{
    var textBox = body.GetControl("dot:TextBox");
    textBox.GetProperty("@Text")
        .HasBinding("NewItemText");
}
{
    var addButton = body.GetControl("dot:Button");
    addButton.GetProperty("@Click")
        .HasBinding("AddItem()", BindingKind.Command);
}