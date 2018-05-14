using System.ComponentModel.DataAnnotations;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class CustomerDetailViewModel
    {
        //add Required attribute
        public string City { get; set; }
        //add Required attribute
        public string ZIP { get; set; }
        //add EmailAddress attribute
        public string Email { get; set; }
    }
}