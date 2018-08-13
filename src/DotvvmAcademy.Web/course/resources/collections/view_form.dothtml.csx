#load "./view_stub.dothtml.csx"

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