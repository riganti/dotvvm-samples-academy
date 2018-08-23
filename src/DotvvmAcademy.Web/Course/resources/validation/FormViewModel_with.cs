using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Validation;

namespace DotvvmAcademy.Course.Account
{
    public class FormViewModel : DotvvmViewModelBase, IValidatableObject
    {
        private readonly LoginFacade loginFacade;
        private readonly RegistrationFacade registrationFacade;

        public FormViewModel(LoginFacade loginFacade, RegistrationFacade registrationFacade)
        {
            this.loginFacade = loginFacade;
            this.registrationFacade = registrationFacade;
        }

        public LoginDTO Login { get; set; }

        public RegistrationDTO Registration { get; set; }

        public Task LogIn()
        {
            return loginFacade.LogIn(Login);
        }

        public Task Register()
        {
            return registrationFacade.Register(Registration);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (Context.ModelState.ValidationTarget == Registration)
            {
                if (Registration == null || string.IsNullOrEmpty(Registration.Password) || Registration.Password.Length < 16)
                {
                    yield return this.CreateValidationResult("Your password is too short.", v => v.Registration.Password);
                }

                if(!Registration.Password.Contains('$')
                    || !Registration.Password.Contains('_')
                    || !Registration.Password.Contains("pumpkin"))
                {
                    yield return this.CreateValidationResult("Your password doesn't contain required characters.", v => v.Registration.Password);
                }
            }
        }
    }
}
