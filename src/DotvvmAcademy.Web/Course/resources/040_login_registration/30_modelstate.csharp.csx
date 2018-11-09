#load "10_loginform_attributes.csharp.csx"

using DotVVM.Framework.Hosting;

Unit.SetDefault("LogInRegistrationViewModel_30.cs");
Unit.SetCorrect("LogInRegistrationViewModel_40.cs");

Unit.GetMethod("DotVVM.Framework.ViewModel.Validation.ValidationErrorFactory.AddModelError")
    .Allow();

Unit.Run(context =>
{
    var accountService = context.Instantiate(AccountService);
    var vm = context.Instantiate(ViewModel, accountService);
    vm.Context = new DotvvmRequestContext();
    vm.LogInForm.Email = $"{Guid.NewGuid()}@example.com";
    vm.LogInForm.Password = Guid.NewGuid().ToString();
    vm.LogIn();
    if (vm.Context.ModelState.Errors.Count == 0)
    {
        context.Report("You must create an error in the 'LogIn' command.");
    }
});