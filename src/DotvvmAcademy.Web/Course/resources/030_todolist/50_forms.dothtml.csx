#load "./30_repeater.dothtml.csx"

Unit.SetDefault("ToDo_20.dothtml");
Unit.SetCorrect("ToDo_30.dothtml");

Unit.GetControl("/html/body/dot:TextBox")
    .GetProperty("@Text")
    .HasBinding("NewItem");

Unit.GetControl("/html/body/dot:Button")
    .GetProperty("@Click")
    .HasBinding("Add()", BindingKind.Command);

repeater.GetControl("@ItemTemplate/dot:Button")
    .GetProperty("@Click")
    .HasBinding("_root.Remove(_this)", BindingKind.Command);