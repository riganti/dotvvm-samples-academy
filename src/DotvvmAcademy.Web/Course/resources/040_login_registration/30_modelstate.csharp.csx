#load "10_loginform_attributes.csharp.csx"

using System;
using System.Collections.Generic;
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
    context.Report("Dynamic validation is currently broken at 30_modelstate.");
    // var accountService = context.Instantiate(AccountService);
    // var vm = context.Instantiate(ViewModel, accountService);
    // vm.Context = new DotvvmRequestContext();
    // vm.Context.Configuration = DotvvmConfiguration.CreateDefault();
    // vm.Context.ModelState.ValidationTarget = vm;
    // vm.Init().GetAwaiter().GetResult();
    // if (vm.LogInForm == null)
    // {
    //     context.Report("You must initialize the 'LogInForm' property in the 'Init' method.");
    //     return;
    // }
    // vm.LogInForm.Email = $"{Guid.NewGuid()}@example.com";
    // vm.LogInForm.Password = Guid.NewGuid().ToString();
    // vm.LogIn();
    // if ((vm.Context.ModelState.Errors?.Count ?? 0) == 0)
    // {
    //     context.Report("You must create an error in the 'LogIn' command.");
    // }
});