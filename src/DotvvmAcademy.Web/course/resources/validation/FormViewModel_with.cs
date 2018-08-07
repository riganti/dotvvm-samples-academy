using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Course
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
            if (Context.ModelState.ValidationTarget != Registration)
            {
                return;
            }

            if (Registration == null 
                || string.IsNullOrEmpty(Registration.Password) 
                || Registration.Password.Length < 16)
            {
                yield return CreateValidationError(v => v.Registration.Password, "Your password is too short.");
            }
            if(!Registration.Password.Contains('$')
                || !Registration.Password.Contains('_'))
            {
                yield return CreateValidationError(v => v.Registration.Password, "Your password doesn't contain required characters.");
            }
        }
    }
}
