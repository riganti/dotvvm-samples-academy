#load "./constants.csx"

using System.ComponentModel.DataAnnotations;

Unit.SetFileName("DTOs.cs");
Unit.SetDefaultCodePath("./DTOs_without.cs");
Unit.SetCorrectCodePath("./DTOs_with.cs");
Unit.SetSourcePath("LoginFacade.cs", "./LoginFacade.cs");
Unit.SetSourcePath("RegistrationFacade.cs", "./RegistrationFacade.cs");

Unit.GetType<string>().Allow();
Unit.GetType<int>().Allow();
Unit.GetType<RequiredAttribute>().GetMethods(".ctor").Allow();
Unit.GetType<EmailAddressAttribute>().GetMethods(".ctor").Allow();
Unit.GetType<RangeAttribute>().GetMethods(".ctor").Allow();

var loginDto = Unit.GetType(LoginDTO)
    .IsTypeKind(TypeKind.Class);
{
    loginDto.GetProperty("Email")
        .IsOfType<string>()
        .HasAttribute(typeof(RequiredAttribute))
        .HasAttribute(typeof(EmailAddressAttribute));
    loginDto.GetProperty("Password")
        .IsOfType<string>()
        .HasAttribute(typeof(RequiredAttribute));
}

var registrationDto = Unit.GetType(RegistrationDTO)
    .IsTypeKind(TypeKind.Class);
{
    registrationDto.GetProperty("Email")
        .IsOfType<string>()
        .HasAttribute(typeof(RequiredAttribute))
        .HasAttribute(typeof(EmailAddressAttribute));
    registrationDto.GetProperty("Password")
        .IsOfType<string>()
        .HasAttribute(typeof(RequiredAttribute));
    registrationDto.GetProperty("Age")
        .IsOfType<int>()
        .HasAttribute(new RangeAttribute(0, 100));
    registrationDto.GetProperty("Address")
        .IsOfType<string>()
        .HasNoAttribute(typeof(RequiredAttribute));
    registrationDto.GetProperty("Phone")
        .IsOfType<string>()
        .HasNoAttribute(typeof(RequiredAttribute));
}