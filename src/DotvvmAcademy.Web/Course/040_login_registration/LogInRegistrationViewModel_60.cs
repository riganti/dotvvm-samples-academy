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

    public class RegistrationForm
    {
        [Required]
        [Range(18, double.MaxValue)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

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
                RegistrationForm = new RegistrationForm();
            }
            return base.Init();
        }

        public LogInForm LogInForm { get; set; }

        public RegistrationForm RegistrationForm { get; set; }

        public void LogIn()
        {
            if (!accountService.LogIn(LogInForm.Email, LogInForm.Password))
            {
                this.AddModelError(vm => vm.LogInForm, "Log-In failed.");
            }
            Context.FailOnInvalidModelState();
        }

        public void Register()
        {
            accountService.Register(
                RegistrationForm.Email,
                RegistrationForm.Password,
                RegistrationForm.Name,
                RegistrationForm.Age);
        }
    }
}