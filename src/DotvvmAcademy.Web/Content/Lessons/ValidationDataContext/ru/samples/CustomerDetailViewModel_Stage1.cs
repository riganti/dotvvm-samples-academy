using System.ComponentModel.DataAnnotations;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class Lesson4ViewModel
    {
        //добавьте атрибут Required
        public string City { get; set; }
        //добавьте атрибут Required
        public string ZIP { get; set; }
        //добавьте атрибут EmailAddress
        public string Email { get; set; }
    }
}