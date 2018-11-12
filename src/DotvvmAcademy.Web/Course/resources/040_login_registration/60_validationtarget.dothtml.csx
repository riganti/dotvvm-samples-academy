#load "50_validationsummary.dothtml.csx"

Unit.SetDefault("LogInRegistration_40.dothtml");
Unit.SetCorrect("LogInRegistration_50.dothtml");

logInSection.GetControl("dot:Button")
    .GetProperty("@Validation.Target")
    .HasBinding("LogInForm");

registerSection.GetControl("dot:Button")
    .GetProperty("@Validation.Target")
    .HasBinding("RegistrationForm");