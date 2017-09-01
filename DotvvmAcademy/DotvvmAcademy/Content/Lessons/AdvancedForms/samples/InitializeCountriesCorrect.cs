using System.Collections.Generic;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class Lesson3ViewModel
    {
        public Lesson3ViewModel()
        {
            Countries.Add(new CountryInfo() { Id = 1, Name = "USA" });
            Countries.Add(new CountryInfo() { Id = 2, Name = "Canada" });
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int SelectedCountryId { get; set; }
        public List<CountryInfo> Countries { get; set; } = new List<CountryInfo>();
    }
}