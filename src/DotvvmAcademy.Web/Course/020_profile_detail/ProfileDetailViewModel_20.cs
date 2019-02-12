namespace DotvvmAcademy.Course.ProfileDetail
{
    public class Profile
    {
        public string FirstName { get; set; } = "John";

        public string LastName { get; set; } = "Smith";

        public string Country { get; set; } = "Wonderland";

        public string City { get; set; } = "Polis";

        public string Street { get; set; } = "North";
    }

    public class ProfileDetailViewModel
    {
        public string NewLastName { get; set; }

        public Profile Profile { get; set; } = new Profile();

        public void Rename()
        {
            Profile.LastName = NewLastName;
        }
    }
}