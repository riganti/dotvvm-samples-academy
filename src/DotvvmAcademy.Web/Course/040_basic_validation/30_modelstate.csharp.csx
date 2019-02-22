#load "10_attributes.csharp.csx"

using System;
using System.Collections.Generic;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Unit;

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
    var accountService = context.Instantiate(AccountServiceName);
    var vm = ViewModel.Instantiate(context, ViewModelName, accountService);
    vm.Email = $"{Guid.NewGuid()}@example.com";
    vm.Password = Guid.NewGuid().ToString();
    try
    {
        vm.LogIn();
    }
    catch(DotvvmInterruptRequestExecutionException e) when (e.Message.Contains("The ViewModel contains validation errors!"))
    {
        return;
    }
    context.Report(NoErrorDiagnosticMessage);
});

Unit.Run(context =>
{
    var accountService = context.Instantiate(AccountServiceName);
    var vm = ViewModel.Instantiate(context, ViewModelName, accountService);
    vm.Email = "john@example.com";
    vm.Password = "CorrectHorseBatteryStaple";
    try
    {
        vm.LogIn();
    }
    catch(DotvvmInterruptRequestExecutionException e) when (e.Message.Contains("The ViewModel contains validation errors!"))
    {
        context.Report(ErrorOnCorrectDiagnosticMessage);
    }
});