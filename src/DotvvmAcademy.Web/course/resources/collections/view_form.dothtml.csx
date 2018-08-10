#load "./view_stub.dothtml.csx"

Unit.GetProperty("/html/body/dot:Repeater/@ItemTemplate/dot:Button/@Click")
    .HasBinding("_root.RemoveItem(Id)", BindingKind.Command);

Unit.GetProperty("/html/body/dot:TextBox/@Text")
    .HasBinding("NewItemText");

Unit.GetProperty("/html/body/dot:Button/@Click")
    .HasBinding("AddItem()", BindingKind.Command);