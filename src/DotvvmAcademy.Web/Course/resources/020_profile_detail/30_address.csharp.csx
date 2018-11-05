#load "./00_constants.csx"

Unit.SetFileName("ProfileDetailViewModel.cs");
Unit.SetDefault("./ProfileDetailViewModel_30.cs");
Unit.SetCorrect("./ProfileDetailViewModel_40.cs");

Unit.GetType<string>()
    .Allow();

Unit.GetType("System.Void")
    .Allow();

var addressType = Unit.GetType(Address);
addressType.Allow();
addressType.GetProperty("Country")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter();
addressType.GetProperty("City")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter();
addressType.GetProperty("Street")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter();

var profileType = Unit.GetType(Profile);
profileType.Allow();
profileType.GetProperty("FirstName")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter();
profileType.GetProperty("LastName")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter()
    .Allow();
profileType.GetProperty("Address")
    .IsOfType(Address)
    .HasGetter()
    .HasSetter();

var viewModelType = Unit.GetType(ProfileDetailViewModel);
viewModelType.GetProperty("Profile")
    .IsOfType(Profile)
    .HasGetter()
    .HasSetter()
    .Allow();
viewModelType.GetProperty("NewLastName")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter()
    .Allow();

Unit.Run(c =>
{
    var vm = c.Instantiate(ProfileDetailViewModel);
    if (vm.Profile == null)
    {
        c.Report("You must initialize the Profile property.");
    }
    if (vm.Profile.Address == null)
    {
        c.Report("You must initialize the Address property.");
    }
    string original = vm.Profile.LastName;
    vm.NewLastName = $"{original}ish";
    vm.Rename();
    if (vm.NewLastName != vm.Profile.LastName) {
        c.Report("Your Rename method doesn't work properly.");
    }
});