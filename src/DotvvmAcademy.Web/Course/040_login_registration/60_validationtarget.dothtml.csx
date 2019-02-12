#load "50_validationsummary.dothtml.csx"

using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.SetDefault("LogInRegistration_40.dothtml");
Unit.SetCorrect("LogInRegistration_50.dothtml");

logInSection.GetControl("dot:Button")
    .GetProperty("@Validation.Target")
    .RequireBinding("LogInForm");

registerSection.GetControl("dot:Button")
    .GetProperty("@Validation.Target")
    .RequireBinding("RegistrationForm");