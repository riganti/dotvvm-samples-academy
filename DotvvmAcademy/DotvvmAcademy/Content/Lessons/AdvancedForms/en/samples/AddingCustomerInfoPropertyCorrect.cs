using System.Collections.Generic;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class Lesson3ViewModel
    {
        public Lesson3ViewModel ()
        {
            Countries.Add(new CountryInfo() { Id = 1, Name = "USA" });
            Countries.Add(new CountryInfo() { Id = 2, Name = "Canada" });
        }

        public CustomerInfo NewCustomer { get; set; }

        public List<DotvvmAcademy.Tutorial.ViewModels.CountryInfo> Countries { get; set; } = new List<DotvvmAcademy.Tutorial.ViewModels.CountryInfo>();
    }
}