#load "./constants.csx"

using DotVVM.Framework.Hosting;
using System.ComponentModel.DataAnnotations;

Unit.AddSourcePath("./DTOs_with.cs");
Unit.AddSourcePath("./LoginFacade.cs");
Unit.AddSourcePath("./RegistrationFacade.cs");
Unit.SetDefaultCodePath("./FormViewModel_without.cs");
Unit.SetCorrectCodePath("./FormViewModel_with.cs");

var formViewModel = Unit.GetType(FormViewModel);
formViewModel.GetProperty("Login").IsOfType(LoginDTO);
formViewModel.GetProperty("Registration").IsOfType(RegistrationDTO);
formViewModel.GetMethod("LogIn").Returns<Task>().HasParameters();
formViewModel.GetMethod("Register").Returns<Task>().HasParameters();
formViewModel.Implements<IValidatableObject>();
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