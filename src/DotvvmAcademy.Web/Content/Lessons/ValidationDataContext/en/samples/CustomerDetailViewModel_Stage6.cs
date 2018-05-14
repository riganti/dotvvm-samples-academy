using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class CustomerDetailViewModel : IValidatableObject
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string ZIP { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime SubscriptionFrom { get; set; }
        public DateTime SubscriptionTo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SubscriptionFrom > SubscriptionTo)
            {
                yield return new ValidationResult("SubscriptionFrom should be less that SubscriptionTo!");
            }
        }
    }
}
