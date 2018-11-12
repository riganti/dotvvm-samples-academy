#load "./20_view.dothtml.csx"

Unit.SetDefault("./Calculator_20.dothtml");
Unit.SetCorrect("./Calculator_30.dothtml");

body.GetControl("dot:TextBox[1]")
    .GetProperty("@Text")
    .HasBinding("Number1");

body.GetControl("dot:TextBox[2]")
    .GetProperty("@Text")
    .HasBinding("Number2");