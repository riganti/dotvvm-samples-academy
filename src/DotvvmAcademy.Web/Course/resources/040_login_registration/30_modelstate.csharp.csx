#load "10_loginform_attributes.csharp.csx"

using System;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel.Validation;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.SetDefault("LogInRegistrationViewModel_30.cs");
Unit.SetCorrect("LogInRegistrationViewModel_40.cs");

Unit.GetType(typeof(ValidationErrorFactory))
    .GetMethods(nameof(ValidationErrorFactory.AddModelError))
    .Allow();

Unit.GetType(typeof(DotvvmRequestContextExtensions))
    .GetMethods(nameof(DotvvmRequestContextExtensions.FailOnInvalidModelState))
    .Allow();

Unit.Run(context =>
{
    var accountService = context.Instantiate(AccountService);
    var vm = context.Instantiate(ViewModel, accountService);
    vm.Context = new DotvvmRequestContext();
    vm.Context.Configuration = DotvvmConfiguration.CreateDefault();
    vm.Context.ModelState.ValidationTarget = vm;
    vm.Init().GetAwaiter().GetResult();
    if (vm.LogInForm == null)
    {
        context.Report("You must initialize the 'LogInForm' property in the 'Init' method.");
        return;
    }
    vm.LogInForm.Email = $"{Guid.NewGuid()}@example.com";
    vm.LogInForm.Password = Guid.NewGuid().ToString();
    vm.LogIn();
    if ((vm.Context.ModelState.Errors?.Count ?? 0) == 0)
    {
        context.Report("You must create an error in the 'LogIn' command.");
    }
});