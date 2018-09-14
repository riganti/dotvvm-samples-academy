Unit.SetFileName("View.dothtml");
Unit.SetDefault("./View_10.dothtml");
Unit.SetCorrect("./View_20.dothtml");

Unit.GetControl("/html/body/dot:TextBox")
    .GetProperty("Text")
    .HasBinding("Text");