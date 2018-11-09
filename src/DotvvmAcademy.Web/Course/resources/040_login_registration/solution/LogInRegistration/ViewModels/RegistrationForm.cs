using DotVVM.Framework.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace DotvvmAcademy.Course.LogInRegistration.ViewModels
{
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
}