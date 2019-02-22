#load "00_constants.csx"

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Validation;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.CSharp;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.GetType(typeof(void))
    .Allow();

Unit.GetType<string>()
    .Allow();

var emailAddress = Unit.GetType<EmailAddressAttribute>()
    .Allow();
{
    emailAddress.GetMethod(".ctor")
        .Allow();
}

var required = Unit.GetType<RequiredAttribute>()
    .Allow();
{
    required.GetMethod(".ctor")
        .Allow();
}

var viewModelBase = Unit.GetType<DotvvmViewModelBase>()
    .Allow();
{
    viewModelBase.GetProperties("Context")
        .Allow();
}

var accountService = Unit.GetType(AccountServiceName)
    .Allow();
{
    accountService.GetMethod("LogIn").Allow();
}

var viewModel = Unit.GetType(ViewModelName);
{
    viewModel.GetField(AccountServiceField)
        .Allow();

    viewModel.GetMethod(LogInMethod)
        .Allow();

    viewModel.GetProperty(EmailProperty)
        .Allow()
        .RequireType<string>()
        .RequireAccess(AllowedAccess.Public)
        .RequireGetter()
        .RequireSetter()
        .RequireAttribute<EmailAddressAttribute>()
        .RequireAttribute<RequiredAttribute>();

    viewModel.GetProperty(PasswordProperty)
        .Allow()
        .RequireType<string>()
        .RequireAccess(AllowedAccess.Public)
        .RequireGetter()
        .RequireSetter()
        .RequireAttribute<RequiredAttribute>();
}
