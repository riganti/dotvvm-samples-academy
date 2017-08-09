using System.Collections.Generic;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class Lesson3ViewModel
    {
        public Lesson3ViewModel()
        {
            // initialize the countries
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int SelectedCountryId { get; set; }
        public List<CountryInfo> Countries { get; set; } = new List<CountryInfo>();
    }
}