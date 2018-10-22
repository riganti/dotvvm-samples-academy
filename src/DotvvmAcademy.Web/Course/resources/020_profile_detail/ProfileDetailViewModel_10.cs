namespace DotvvmAcademy.Course.ProfileDetail
{
    public class Profile
    {

    }

    public class ProfileDetailViewModel
    {
        public string NewLastName { get; set; }

        public string FirstName { get; set; } = "John";

        public string LastName { get; set; } = "Smith";

        public string Country { get; set; } = "Wonderland";

        public string City { get; set; } = "Polis";

        public string Street { get; set; } = "North";

        public void Rename()
        {
            LastName = NewLastName;
        }
    }
}