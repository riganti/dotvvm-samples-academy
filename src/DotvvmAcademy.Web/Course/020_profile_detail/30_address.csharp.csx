#load "./00_constants.csx"

using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.SetDefault("ProfileDetailViewModel_30.cs");
Unit.SetCorrect("ProfileDetailViewModel_40.cs");

Unit.GetType<string>()
    .Allow();

Unit.GetType(typeof(void))
    .Allow();

var addressType = Unit.GetType(Address);
addressType.Allow();
addressType.GetProperty("Country")
    .RequireType<string>()
    .RequireGetter()
    .RequireSetter();
addressType.GetProperty("City")
    .RequireType<string>()
    .RequireGetter()
    .RequireSetter();
addressType.GetProperty("Street")
    .RequireType<string>()
    .RequireGetter()
    .RequireSetter();

var profileType = Unit.GetType(Profile);
profileType.Allow();
profileType.GetProperty("FirstName")
    .RequireType<string>()
    .RequireGetter()
    .RequireSetter();
profileType.GetProperty("LastName")
    .RequireType<string>()
    .RequireGetter()
    .RequireSetter()
    .Allow();
profileType.GetProperty("Address")
    .RequireType(Address)
    .RequireGetter()
    .RequireSetter();

var viewModelType = Unit.GetType(ProfileDetailViewModel);
viewModelType.GetProperty("Profile")
    .RequireType(Profile)
    .RequireGetter()
    .RequireSetter()
    .Allow();
viewModelType.GetProperty("NewLastName")
    .RequireType<string>()
    .RequireGetter()
    .RequireSetter()
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