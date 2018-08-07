#load "./constants.csx"

using System.ComponentModel.DataAnnotations;

Unit.AddSource("./LoginFacade.cs");
Unit.AddSource("./RegistrationFacade.cs");
Unit.SetDefaultCodePath("./DTOs_without.cs");
Unit.SetCorrectCodePath("./DTOs_with.cs");

var loginDto = Unit.GetType(LoginDTO)
    .IsTypeKind(CSharpTypeKind.Class);

loginDto.GetProperty("Email")
    .IsOfType<string>()
    .HasAttribute<RequiredAttribute>()
    .HasAttribute<EmailAddressAttribute>();

loginDto.GetProperty("Password")
    .IsOfType<string>()
    .HasAttribute<RequiredAttribute>();

var registrationDto = Unit.GetType(RegistrationDTO)
    .IsTypeKind(CSharpTypeKind.Class);

registrationDto.GetProperty("Email")
    .IsOfType<string>()
    .HasAttribute<RequiredAttribute>()
    .HasAttribute<EmailAddressAttribute>();

registrationDto.GetProperty("Password")
    .IsOfType<string>()
    .HasAttribute<RequiredAttribute>();

registrationDto.GetProperty("Age")
    .IsOfType<int>()
    .HasAttribute<RangeAttribute>(new { Minimum = 0, Maximum = 100 });

registrationDto.GetProperty("Address")
    .IsOfType<string>()
    .HasNoAttribute<RequiredAttribute>();

registrationDto.GetProperty("Phone")
    .IsOfType<string>()
    .HasNoAttribute<RequiredAttribute>();