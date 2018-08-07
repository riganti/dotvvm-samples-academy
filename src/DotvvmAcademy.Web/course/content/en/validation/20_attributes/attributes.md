# Attributes

We'll validate a Log In form and a Registration form that are next to each other in a single View.

Our ViewModel currently has this shape (without the BL calls) :

```csharp
namespace DotvvmAcademy.Course
{
    public class FormViewModel
    {
        public LoginDTO Login { get; set; }
        
        public RegistrationDTO Registration { get; set; }
    }
}
```

DotVVM recognizes attributes from the `System.ComponentModel.DataAnnotations` namespace. For instance the:

- [RequiredAttribute] - value must not be null or empty
- [RangeAttribute] - numeric value must be within certain range
- [EmailAddressAttribute] - string must be in the format of an email address
- [RegularExpressionAttribute] - string must match a regular expression


---

Let's start by adding some attributes to our DTOs:

- Add `Required` to everything but `Address` and `Phone`
- Add `EmailAddress` where it's obviously needed
- Add `Range` on `Age` and limit it to [0, 100]

[RequiredAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.requiredattribute
[RangeAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.rangeattribute
[EmailAddressAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.emailaddressattribute
[RegularExpressionAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.regularexpressionattribute

[CodeTask](/resources/validation/dto_attributes.csharp.csx)
