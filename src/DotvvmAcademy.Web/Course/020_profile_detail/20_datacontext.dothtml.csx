#load "00_constants.csx"

using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

public DothtmlUnit Unit { get; set; } = new DothtmlUnit();

Unit.SetDefault("ProfileDetail_10.dothtml");
Unit.SetCorrect("ProfileDetail_20.dothtml");
Unit.AddDependency("ProfileDetailViewModel_20.cs");

Unit.GetDirective("/@viewModel")
    .RequireTypeArgument(ProfileDetailViewModel);

var div = Unit.GetControl("/html/body/div");
var inner = div.GetControl("div");
{
    div.GetProperty("@DataContext")
        .RequireBinding("Profile");

    div.GetControl("p[1]/dot:Literal")
        .GetProperty("@Text")
        .RequireBinding("FirstName");

    div.GetControl("p[2]/dot:Literal")
        .GetProperty("@Text")
        .RequireBinding("LastName");
    {
        inner.GetControl("p[1]/dot:Literal")
            .GetProperty("@Text")
            .RequireBinding("Country");

        inner.GetControl("p[2]/dot:Literal")
            .GetProperty("@Text")
            .RequireBinding("City");

        inner.GetControl("p[3]/dot:Literal")
            .GetProperty("@Text")
            .RequireBinding("Street");
    }
}