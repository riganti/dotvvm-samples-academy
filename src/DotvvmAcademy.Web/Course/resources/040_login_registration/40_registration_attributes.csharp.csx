#load "30_modelstate.csharp.csx"

using System.ComponentModel.DataAnnotations;

Unit.SetDefault("LogInRegistrationViewModel_50.cs");
Unit.SetCorrect("LogInRegistrationViewModel_60.cs");

Unit.GetType<int>()
    .Allow();

Unit.GetType<double>()
    .Allow()
    .GetField("MaxValue")
    .Allow();

Unit.GetType<RangeAttribute>()
    .Allow()
    .GetMethods(".ctor")
    .Allow();

viewModel.GetProperty("RegistrationForm")
    .IsOfType(RegistrationForm)
    .Allow();

accountService.GetMethod("Register")
    .Allow();

var registrationForm = Unit.GetType(RegistrationForm);
registrationForm.Allow();
registrationForm.GetProperty("Age")
    .IsOfType<int>()
    .HasAttribute(typeof(RequiredAttribute))
    .HasAttribute(typeof(RangeAttribute))
    .Allow();

registrationForm.GetProperty("Email")
    .IsOfType<string>()
    .HasAttribute(typeof(RequiredAttribute))
    .HasAttribute(typeof(EmailAddressAttribute))
    .Allow();

registrationForm.GetProperty("Name")
    .IsOfType<string>()
    .HasAttribute(typeof(RequiredAttribute))
    .Allow();

registrationForm.GetProperty("Password")
    .IsOfType<string>()
    .HasAttribute(typeof(RequiredAttribute))
    .Allow();