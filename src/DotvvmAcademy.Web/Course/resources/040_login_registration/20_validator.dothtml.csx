#load "00_constants.csx"
Unit.SetFileName("LogInRegistration.dothtml");
Unit.SetDefault("LogInRegistration_10.dothtml");
Unit.SetCorrect("LogInRegistration_20.dothtml");

Unit.GetDirective("/@viewModel")
    .HasTypeArgument(ViewModel);

var logInSection = Unit.GetControl("/html/body/section[1]");
var emailValidator = logInSection.GetControl("div[1]//dot:Validator");
emailValidator.GetProperty("@Value")
    .HasBinding("LogInForm.Email");
emailValidator.GetProperty("@ShowErrorMessageText")
    .HasValue(true);

var passwordValidator = logInSection.GetControl("div[2]//dot:Validator");
passwordValidator.GetProperty("@Value")
    .HasBinding("LogInForm.Password");
emailValidator.GetProperty("@ShowErrorMessageText")
    .HasValue(true);