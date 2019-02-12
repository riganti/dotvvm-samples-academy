#load "./00_constants.csx"

using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

public DothtmlUnit Unit { get; set; } = new DothtmlUnit();

Unit.SetDefault("ToDo_10.dothtml");
Unit.SetCorrect("ToDo_20.dothtml");
Unit.AddDependency("ToDoViewModel_40.cs");

Unit.GetDirective("/@viewModel")
    .RequireTypeArgument(ToDoViewModel);

var repeater = Unit.GetControl("/html/body/dot:Repeater");

repeater.GetProperty("@DataSource")
    .RequireBinding("Items");

repeater.GetControl("p/dot:Literal")
    .GetProperty("@Text")
    .RequireBinding("_this");