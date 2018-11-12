using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Validation;

namespace DotvvmAcademy.Course.LogInRegistration
{
    public class LogInForm
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LogInRegistrationViewModel : DotvvmViewModelBase
    {
        private readonly AccountService accountService;

        public LogInRegistrationViewModel(AccountService logInService)
        {
            accountService = logInService;
        }

        public override Task Init()
        {
            if (!Context.IsPostBack)
            {
                LogInForm = new LogInForm();
            }
            return base.Init();
        }

        public LogInForm LogInForm { get; set; }

        public void LogIn()
        {
            accountService.LogIn(LogInForm.Email, LogInForm.Password);
        }
    }
}