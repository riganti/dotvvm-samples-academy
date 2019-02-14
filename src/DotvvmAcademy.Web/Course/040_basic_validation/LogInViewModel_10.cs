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

        public string Email { get; set; }

        public string Password { get; set; }

        public void LogIn()
        {
            
        }
    }
}