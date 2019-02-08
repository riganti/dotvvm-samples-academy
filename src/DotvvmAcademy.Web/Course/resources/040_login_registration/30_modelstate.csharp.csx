#load "10_loginform_attributes.csharp.csx"

using System;
using System.Collections.Generic;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.SetDefault("LogInRegistrationViewModel_30.cs");
Unit.SetCorrect("LogInRegistrationViewModel_40.cs");

var boolType = Unit.GetType<bool>()
    .Allow();
boolType.GetMethod("ToString")
    .Allow();

Unit.GetType(typeof(ValidationErrorFactory))
    .GetMethods(nameof(ValidationErrorFactory.AddModelError))
    .Allow();

Unit.GetType(typeof(DotvvmRequestContextExtensions))
    .GetMethods(nameof(DotvvmRequestContextExtensions.FailOnInvalidModelState))
    .Allow();

Unit.GetType<IDotvvmRequestContext>()
    .GetProperties(nameof(IDotvvmRequestContext.ModelState))
    .Allow();

Unit.GetType<ModelState>()
    .GetProperties(nameof(ModelState.Errors))
    .Allow();

Unit.GetType(typeof(List<>))
    .GetMethods(nameof(List<object>.Add))
    .Allow();

Unit.GetType<ViewModelValidationError>()
    .Allow()
    .GetProperties(nameof(ViewModelValidationError.ErrorMessage))
    .Allow();

Unit.Run(context =>
{
    var accountService = context.Instantiate(AccountService);
    var vm = CSharpDynamicContextExtensions.InstantiateViewModel(context, ViewModel, accountService);
    vm.Init().GetAwaiter().GetResult();
    if (vm.LogInForm == null)
    {
        context.Report("You must initialize the 'LogInForm' property in the 'Init' method.");
        return;
    }
    vm.LogInForm.Email = $"{Guid.NewGuid()}@example.com";
    vm.LogInForm.Password = Guid.NewGuid().ToString();
    try
    {
        vm.LogIn();
    }
    catch(DotvvmInterruptRequestExecutionException e) when (e.Message.Contains("The ViewModel contains validation errors!"))
    {
        return;
    }
    context.Report("You must create an error in the 'LogIn' command.");
});