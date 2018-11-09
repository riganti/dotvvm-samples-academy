#load "40_registration_attributes.csharp.csx"

using DotVVM.Framework.Hosting;

Unit.SetDefault("LogInRegistrationViewModel_60.cs");
Unit.SetCorrect("LogInRegistrationViewModel_70.cs");

Unit.Run(context =>
{
    var accountService = context.Instantiate(AccountService);
    var vm = context.Instantiate(ViewModel, accountService);
    vm.Context = new DotvvmRequestContext();
    vm.Register();
    if (vm.Context.ModelState.Errors.Count == 0)
    {
        context.Report("You must create an error in the 'Register' Command.");
    }
});