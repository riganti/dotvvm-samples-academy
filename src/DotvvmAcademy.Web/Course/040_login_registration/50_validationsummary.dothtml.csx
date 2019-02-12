#load "20_validator.dothtml.csx"

using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.SetDefault("LogInRegistration_30.dothtml");
Unit.SetCorrect("LogInRegistration_40.dothtml");

var registerSection = Unit.GetControl("/html/body/section[2]");
var validationSummary = registerSection.GetControl(".//dot:ValidationSummary");
validationSummary.GetProperty("@Validation.Target")
    .RequireBinding("RegistrationForm");
validationSummary.GetProperty("@IncludeErrorsFromTarget")
    .RequireValue(true);