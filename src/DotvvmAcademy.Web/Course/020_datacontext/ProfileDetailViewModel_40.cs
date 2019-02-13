namespace DotvvmAcademy.Course.ProfileDetail
{
    public class Profile
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class ProfileDetailViewModel
    {
        public Profile Profile { get; set; }

        public void Create()
        {
            Profile = new Profile();
        }

        public void Delete()
        {
            Profile = null;
        }
    }
}