#load "30_modelstate.csharp.csx"

using System.ComponentModel.DataAnnotations;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

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
    .RequireType(RegistrationForm)
    .Allow();

accountService.GetMethod("Register")
    .Allow();

var registrationForm = Unit.GetType(RegistrationForm);
registrationForm.Allow();
registrationForm.GetProperty("Age")
    .RequireType<int>()
    .RequireAttribute(typeof(RequiredAttribute))
    .RequireAttribute(typeof(RangeAttribute))
    .Allow();

registrationForm.GetProperty("Email")
    .RequireType<string>()
    .RequireAttribute(typeof(RequiredAttribute))
    .RequireAttribute(typeof(EmailAddressAttribute))
    .Allow();

registrationForm.GetProperty("Name")
    .RequireType<string>()
    .RequireAttribute(typeof(RequiredAttribute))
    .Allow();

registrationForm.GetProperty("Password")
    .RequireType<string>()
    .RequireAttribute(typeof(RequiredAttribute))
    .Allow();