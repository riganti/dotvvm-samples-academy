using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Lessons.AdvancedForms.ViewModels
{
    public class CustomerDetailViewModel
    {
        public CustomerDetailViewModel()
        {
            Interests = new List<string>();

            // initialize the Countries collection
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }
        
        public List<string> Interests { get; set; }

        public int SelectedCountryId { get; set; }

        public List<CountryInfo> Countries { get; set; }
    }
}