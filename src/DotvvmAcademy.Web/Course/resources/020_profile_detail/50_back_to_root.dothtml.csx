#load "./20_datacontext.dothtml.csx"

Unit.SetFileName("ProfileDetail.dothtml");
Unit.SetDefault("./ProfileDetail_30.dothtml");
Unit.SetCorrect("./ProfileDetail_40.dothtml");

div.GetControl("dot:TextBox")
    .GetProperty("@Text")
    .HasBinding("_root.NewLastName");

div.GetControl("dot:Button")
    .GetProperty("@Click")
    .HasBinding("_root.Rename()", BindingKind.Command);