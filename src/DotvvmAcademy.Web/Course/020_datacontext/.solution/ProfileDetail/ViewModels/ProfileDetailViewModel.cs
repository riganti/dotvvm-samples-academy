namespace DotvvmAcademy.Course.ProfileDetail
{
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