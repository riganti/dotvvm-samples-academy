using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Validation;

namespace DotvvmAcademy.Course
{
    public class FormViewModel : DotvvmViewModelBase
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
    }
}
