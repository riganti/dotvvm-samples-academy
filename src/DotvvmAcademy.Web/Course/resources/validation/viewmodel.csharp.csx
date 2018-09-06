#load "./constants.csx"

using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using System.ComponentModel.DataAnnotations;

// CourseFormat metadata

Unit.SetFileName("FormViewModel.cs");
Unit.SetDefaultCodePath("./FormViewModel_without.cs");
Unit.SetCorrectCodePath("./FormViewModel_with.cs");
Unit.SetSourcePath("DTOs.cs", "./DTOs_with.cs");
Unit.SetSourcePath("LoginFacade.cs", "./LoginFacade.cs");
Unit.SetSourcePath("RegistrationFacade.cs", "./RegistrationFacade.cs");

// Allowed symbols
{
    var loginFacade = Unit.GetType(LoginFacade).Allow();
    loginFacade.GetMethods("LogIn").Allow();
    var registrationFacade = Unit.GetType(RegistrationFacade).Allow();
    registrationFacade.GetMethods("Register").Allow();
    var viewModelBase = Unit.GetType<DotvvmViewModelBase>().Allow();
    viewModelBase.GetProperty("Context").Allow();
    var dotvvmRequestContext = Unit.GetType<IDotvvmRequestContext>().Allow();
    dotvvmRequestContext.GetProperty("ModelState").Allow();
    var modelState = Unit.GetType<ModelState>().Allow();
    modelState.GetProperty("ValidationTarget").Allow();
    var @string = Unit.GetType<string>().Allow();
    @string.GetMethods("IsNullOrEmpty").Allow();
    @string.GetProperty("Length").Allow();
    @string.GetMethods("Contains").Allow();
    var registrationDto = Unit.GetType(RegistrationDTO).Allow();
    registrationDto.GetProperty("Password").Allow();
    Unit.GetMethods("DotVVM.Framework.ViewModel.Validation.ValidationErrorFactory::CreateValidationResult").Allow();
    Unit.GetType(LoginDTO).Allow();
    Unit.GetType<Task>().Allow();
    Unit.GetType<IValidatableObject>().Allow();
    Unit.GetType<ValidationResult>().Allow();
    Unit.GetType<ValidationContext>().Allow();
}


var formViewModel = Unit.GetType(FormViewModel)
    .Implements<IValidatableObject>();
formViewModel.GetField("loginFacade")
    .IsOfType(LoginFacade)
    .Allow();
formViewModel.GetField("registrationFacade")
    .IsOfType(RegistrationFacade)
    .Allow();
formViewModel.GetProperty("Login")
    .IsOfType(LoginDTO)
    .Allow();
formViewModel.GetProperty("Registration")
    .IsOfType(RegistrationDTO)
    .Allow();
formViewModel.GetMethod("LogIn")
    .Returns<Task>()
    .HasParameters();
formViewModel.GetMethod("Register")
    .Returns<Task>()
    .HasParameters();
formViewModel.GetMethod("Validate")
    .Returns<IEnumerable<ValidationResult>>()
    .HasParameters<ValidationContext>();

Unit.Run(context =>
{
    var loginFacade = context.Instantiate(LoginFacade);
    var registrationFacade = context.Instantiate(RegistrationFacade);
    var viewModel = context.Instantiate(FormViewModel, loginFacade, registrationFacade);
    viewModel.Context = new DotvvmRequestContext();
    var registration = context.Instantiate(RegistrationDTO);
    registration.Password = "test";
    var errors = viewModel.Validate(null).ToList();
    if (errors.Count != 2 || errors[0].ErrorMessage == errors[1].ErrorMessage)
    {
        context.Report("Your Validate method doesn't fulfil the requirements.");
    }
});