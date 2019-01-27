#load "00_constants.csx"

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Validation;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.SetDefault("LogInRegistrationViewModel_10.cs");
Unit.SetCorrect("LogInRegistrationViewModel_20.cs");
Unit.AddDependency("./solution/LogInRegistration/AccountService.cs");

Unit.GetType(typeof(void))
    .Allow();

Unit.GetType<string>()
    .Allow();
Unit.GetType<Task>()
    .Allow();
Unit.GetType<EmailAddressAttribute>()
    .Allow()
        .GetMethod(".ctor")
        .Allow();
Unit.GetType<RequiredAttribute>()
    .Allow()
        .GetMethod(".ctor")
        .Allow();
var vmBase = Unit.GetType<DotvvmViewModelBase>()
    .Allow();
vmBase.GetProperty("Context")
    .Allow();
vmBase.GetMethod("Init")
    .Allow();
var accountService = Unit.GetType(AccountService).Allow();
accountService.GetMethod("LogIn").Allow();
var context = Unit.GetType<IDotvvmRequestContext>().Allow();
context.GetProperty("IsPostBack").Allow();
Unit.GetMethod("DotvvmRequestContextExtensions.FailOnInvalidModelState");
var viewModel = Unit.GetType(ViewModel);
viewModel.GetField("accountService").Allow();
viewModel.GetMethod("LogIn").Allow();
viewModel.GetProperty("LogInForm").Allow();

var logInForm = Unit.GetType(LogInForm).Allow();
logInForm.GetMethod(".ctor").Allow();
logInForm.GetProperty("Email")
    .RequireType<string>()
    .RequireAttribute(typeof(EmailAddressAttribute))
    .RequireAttribute(typeof(RequiredAttribute))
    .Allow();

logInForm.GetProperty("Password")
    .RequireType<string>()
    .RequireAttribute(typeof(RequiredAttribute))
    .Allow();
