using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Validation;

namespace DotvvmAcademy.Course.LogInRegistration.ViewModels
{
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
            bool success = accountService.LogIn(LogInForm.Email, LogInForm.Password);
            if (!success)
            {
                Context.ModelState.Errors.Add(new ViewModelValidationError
                {
                    ErrorMessage = "Log-In was unsuccessful."
                });
                Context.FailOnInvalidModelState();
            }
        }

        public void Register()
        {
            if (RegistrationForm.Password.Length < 8)
            {
                Context.ModelState.Errors.Add(new ViewModelValidationError
                {
                    ErrorMessage = "Password is too short.",
                    PropertyPath = "Password"
                });
                Context.FailOnInvalidModelState();
            }
            bool success = accountService.Register(
                RegistrationForm.Email,
                RegistrationForm.Password,
                RegistrationForm.Name,
                RegistrationForm.Age);
            if (!success)
            {
                Context.ModelState.Errors.Add(new ViewModelValidationError
                {
                    ErrorMessage = "Registration was unsuccessful."
                });
                Context.FailOnInvalidModelState();
            }
        }
    }
}