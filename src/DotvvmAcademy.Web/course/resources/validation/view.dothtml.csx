#load "./constants.csx"

Unit.AddSourcePath("./FormViewModel_without.cs");
Unit.SetDefaultCodePath("./form_without.dothtml");
Unit.SetCorrectCodePath("./form_with.dothtml");

Unit.GetControl("/child::node()[1]")
    .IsOfType<RawLiteral>()
    .HasRawContent("<!doctype html>", false);

{
    var loginForm = Unit.GetControl("/html/body/div[1]");
    loginForm.GetProperty("Validation.Target").HasBinding("Login");
    loginForm.GetControl("h1").HasRawContent("Log In", false);
    {
        var emailValidator = loginForm.GetControl("div[1]/dot:Validator");
        emailValidator.GetProperty("Value").HasBinding("Login.Email");
        emailValidator.GetProperty("ShowErrorMessageText").HasValue(true);
    }
    {
        var emailTextBox = loginForm.GetControl("div[1]/dot:TextBox");
        emailTextBox.GetProperty("Text").HasBinding("Login.Email");
    }
    {
        var passwordValidator = loginForm.GetControl("div[2]/dot:Validator");
        passwordValidator.GetProperty("Value").HasBinding("Login.Password");
        passwordValidator.GetProperty("ShowErrorMessageText").HasValue(true);
    }
    {
        var passwordTextBox = loginForm.GetControl("div[2]/dot:TextBox");
        passwordTextBox.GetProperty("Text").HasBinding("Login.Password");
    }
    {
        var button = loginForm.GetControl("dot:Button");
        button.GetProperty("Click").HasBinding("LogIn()", BindingKind.Command);
        button.GetProperty("Validation.Enabled").HasValue(true);
    }
}
{
    var registrationForm = Unit.GetControl("/html/body/div[2]");
    registrationForm.GetProperty("Validation.Target").HasBinding("Registration");
    registrationForm.GetControl("h1").HasRawContent("Register", false);
    registrationForm.GetControls("div/dot:TextBox").CountEquals(5);
    {
        var button = registrationForm.GetControl("dot:Button");
        button.GetProperty("Click").HasBinding("Register()", BindingKind.Command);
        button.GetProperty("Validation.Enabled").HasValue(true);
    }
    {
        var validationSummary = registrationForm.GetControl("child::node()[last()]")
            .IsOfType<ValidationSummary>();
        validationSummary.GetProperty("Validation.Target").HasBinding("Registration");
    }
}
