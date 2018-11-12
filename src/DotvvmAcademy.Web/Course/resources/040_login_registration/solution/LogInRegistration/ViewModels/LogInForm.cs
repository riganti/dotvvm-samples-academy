using DotVVM.Framework.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace DotvvmAcademy.Course.LogInRegistration.ViewModels
{
    public class LogInForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}