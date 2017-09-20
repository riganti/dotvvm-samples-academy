using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Lessons.AdvancedForms.ViewModels
{
    public class CustomerDetailViewModel
    {
        public CustomerDetailViewModel()
        {
            Interests = new List<string>();

            Countries = new List<CountryInfo>();
            Countries.Add(new CountryInfo() { Id = 1, Name = "USA" });
            Countries.Add(new CountryInfo() { Id = 2, Name = "Canada" });
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }
        
        public List<string> Interests { get; set; }

        public int SelectedCountryId { get; set; }

        public List<CountryInfo> Countries { get; set; }
    }
}