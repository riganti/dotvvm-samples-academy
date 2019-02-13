#load "00_constants.csx"

using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.SetDefault("ProfileDetailViewModel_10.cs");
Unit.SetCorrect("ProfileDetailViewModel_20.cs");

Unit.GetType<string>()
    .Allow();

var profile = Unit.GetType(ProfileName)
    .Allow();
{
    profile.GetProperty(FirstNameProperty)
        .RequireType<string>()
        .RequireAccess(AllowedAccess.Public)
        .RequireGetter()
        .RequireSetter();

    profile.GetProperty(LastNameProperty)
        .RequireType<string>()
        .RequireAccess(AllowedAccess.Public)
        .RequireGetter()
        .RequireSetter();
}

var viewModel = Unit.GetType(ViewModelName);
{
    viewModel.GetProperties(FirstNameProperty)
        .RequireCount(0);
    
    viewModel.GetProperties(LastNameProperty)
        .RequireCount(0);

    viewModel.GetProperty(ProfileProperty)
        .Allow()
        .RequireType(ProfileName)
        .RequireAccess(AllowedAccess.Public)
        .RequireGetter()
        .RequireSetter();
}