using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Validation;

namespace DotvvmAcademy.Course.LogIn
{
    public class LogInViewModel : DotvvmViewModelBase
    {
        private readonly AccountService accountService;

        public LogInViewModel(AccountService logInService)
        {
            accountService = logInService;
        }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public void LogIn()
        {
            if (!accountService.LogIn(Email, Password))
            {
                this.AddModelError(vm => vm.Email, "Login was unsuccessful.");
                Context.FailOnInvalidModelState();
            }
        }
    }
}