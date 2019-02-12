namespace DotvvmAcademy.Course.ProfileDetail
{
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