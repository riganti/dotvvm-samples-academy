#load "00_constants.csx"

using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

public DothtmlUnit Unit { get; set; } = new DothtmlUnit();

Unit.SetDefault("LogInRegistration_10.dothtml");
Unit.SetCorrect("LogInRegistration_20.dothtml");
Unit.AddDependency("LogInRegistrationViewModel_20.cs");
Unit.AddDependency("./solution/LogInRegistration/AccountService.cs");

Unit.GetDirective("/@viewModel")
    .RequireTypeArgument(ViewModel);

var logInSection = Unit.GetControl("/html/body/section[1]");
var emailValidator = logInSection.GetControl("div[1]//dot:Validator");
emailValidator.GetProperty("@Validator.Value")
    .RequireBinding("LogInForm.Email");
emailValidator.GetProperty("@Validator.ShowErrorMessageText")
    .RequireValue(true);

var passwordValidator = logInSection.GetControl("div[2]//dot:Validator");
passwordValidator.GetProperty("@Validator.Value")
    .RequireBinding("LogInForm.Password");
emailValidator.GetProperty("@Validator.ShowErrorMessageText")
    .RequireValue(true);