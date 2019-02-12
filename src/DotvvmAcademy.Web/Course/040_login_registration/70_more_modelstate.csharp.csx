#load "40_registration_attributes.csharp.csx"

using System;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.SetDefault("LogInRegistrationViewModel_60.cs");
Unit.SetCorrect("LogInRegistrationViewModel_70.cs");

Unit.GetType<bool>()
    .Allow();

Unit.Run(context =>
{
    var accountService = context.Instantiate(AccountService);
    var vm = CSharpDynamicContextExtensions.InstantiateViewModel(context, ViewModel, accountService);
    vm.Init().GetAwaiter().GetResult();
    var id = Guid.NewGuid().ToString("N");
    vm.RegistrationForm.Email = $"{id}@example.com";
    vm.RegistrationForm.Password = id;
    vm.RegistrationForm.Name = id;
    vm.RegistrationForm.
    try
    {
        vm.Register();
    }
    catch(DotvvmInterruptRequestExecutionException e) when (e.Message.Contains("The ViewModel contains validation errors!"))
    {
        context.Report("You must create an error in the 'Register' Command.");
    }
});