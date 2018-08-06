#load "./view_stub.dothtml.csx"

Unit.GetControls("/html/body/dot:Repeater/@ItemTemplate/dot:Button")
    .CountEquals(1);

Unit.GetProperies("/html/body/dot:Repeater/@ItemTemplate/dot:Button/@Click")
    .CountEquals(1)
    .HasBinding("_root.RemoveItem(Id)", BindingKing.Command);

Unit.GetControls("/html/body/dot:TextBox")
    .CountEquals(1);

Unit.GetProperties("/html/body/dot:TextBox/@Text")
    .CountEquals(1)
    .HasBinding("NewItemText");

Unit.GetControls("/html/body/dot:Button")
    .CountEquals(1);

Unit.GetProperties("/html/body/dot:Button/@Click")
    .CountEquals(1)
    .HasBinding("AddItem()", BindingKind.Command);