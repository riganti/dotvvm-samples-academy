#load "./constants.csx"

using System.ComponentModel.DataAnnotations;

Unit.AddSourcePath("./LoginFacade.cs");
Unit.AddSourcePath("./RegistrationFacade.cs");
Unit.SetDefaultCodePath("./DTOs_without.cs");
Unit.SetCorrectCodePath("./DTOs_with.cs");

var loginDto = Unit.GetType(LoginDTO)
    .IsTypeKind(TypeKind.Class);

loginDto.GetProperty("Email")
    .IsOfType<string>()
    .HasAttribute(typeof(RequiredAttribute))
    .HasAttribute(typeof(EmailAddressAttribute));

loginDto.GetProperty("Password")
    .IsOfType<string>()
    .HasAttribute(typeof(RequiredAttribute));

var registrationDto = Unit.GetType(RegistrationDTO)
    .IsTypeKind(TypeKind.Class);

registrationDto.GetProperty("Email")
    .IsOfType<string>()
    .HasAttribute(typeof(RequiredAttribute))
    .HasAttribute(typeof(EmailAddressAttribute));

registrationDto.GetProperty("Password")
    .IsOfType<string>()
    .HasAttribute(typeof(RequiredAttribute));

registrationDto.GetProperty("Age")
    .IsOfType<int>()
    .HasAttribute(typeof(RangeAttribute), new { Minimum = 0, Maximum = 100 });

registrationDto.GetProperty("Address")
    .IsOfType<string>()
    .HasNoAttribute(typeof(RequiredAttribute));

registrationDto.GetProperty("Phone")
    .IsOfType<string>()
    .HasNoAttribute(typeof(RequiredAttribute));