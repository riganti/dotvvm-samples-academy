#load "./00_constants.csx"

Unit.SetFileName("ProfileDetail.dothtml");
Unit.SetDefault("./ProfileDetail_10.dothtml");
Unit.SetCorrect("./ProfileDetail_20.dothtml");

Unit.GetDirective("/@viewModel")
    .HasTypeArgument(ProfileDetailViewModel);

var div = Unit.GetControl("/html/body/div");
var inner = div.GetControl("div");
{
    div.GetProperty("@DataContext")
        .HasBinding("Profile");

    div.GetControl("p[1]/dot:Literal")
        .GetProperty("@Text")
        .HasBinding("FirstName");

    div.GetControl("p[2]/dot:Literal")
        .GetProperty("@Text")
        .HasBinding("LastName");
    {
        inner.GetControl("p[1]/dot:Literal")
            .GetProperty("@Text")
            .HasBinding("Country");

        inner.GetControl("p[2]/dot:Literal")
            .GetProperty("@Text")
            .HasBinding("City");

        inner.GetControl("p[3]/dot:Literal")
            .GetProperty("@Text")
            .HasBinding("Street");
    }
}