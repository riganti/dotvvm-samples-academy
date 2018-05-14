using System.ComponentModel.DataAnnotations;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class CustomerDetailViewModel
    {
        //pøidjte atribut Required 
        public string City { get; set; }
        //pøidjte atribut Required 
        public string ZIP { get; set; }
        //pøidjte atribut EmailAddress
        public string Email { get; set; }
    }
}