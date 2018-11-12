#load "./00_constants.csx"

Unit.SetFileName("ToDo.dothtml");
Unit.SetDefault("./ToDo_10.dothtml");
Unit.SetCorrect("./ToDo_20.dothtml");

Unit.GetDirective("/@viewModel")
    .HasTypeArgument(ToDoViewModel);

var repeater = Unit.GetControl("/html/body/dot:Repeater");

repeater.GetProperty("@DataSource")
    .HasBinding("Items");

repeater.GetControl("p/dot:Literal")
    .GetProperty("@Text")
    .HasBinding("_this");