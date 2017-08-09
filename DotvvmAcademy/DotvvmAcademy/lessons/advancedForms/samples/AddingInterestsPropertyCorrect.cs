using System.Collections.Generic;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class Lesson3ViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public List<string> Interests { get; set; } = new List<string>();
    }
}