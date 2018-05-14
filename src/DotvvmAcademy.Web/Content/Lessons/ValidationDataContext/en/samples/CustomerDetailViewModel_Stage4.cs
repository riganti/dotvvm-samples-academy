using System;
using System.ComponentModel.DataAnnotations;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class CustomerDetailViewModel
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string ZIP { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public DateTime SubscriptionFrom { get; set; }
        public DateTime SubscriptionTo { get; set; }
    }
}