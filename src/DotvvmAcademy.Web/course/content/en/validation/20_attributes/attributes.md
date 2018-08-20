# Attributes

Let's validate a Log In form and a Registration form that are next to each other in a single View.

DotVVM recognizes attributes from the `System.ComponentModel.DataAnnotations` namespace. These attributes provide an easy way to validate data in certain situations. Some of them are listed below.

- [RequiredAttribute] - value must not be null or empty
- [RangeAttribute] - numeric value must be within certain range
- [EmailAddressAttribute] - string must be in the format of an email address
- [RegularExpressionAttribute] - string must match a regular expression

Right now the ViewModel is not important, let's just assume it contains a property of type `LoginDTO` and a property of type `RegistrationDTO`. Let's annotate these two [Data Transfer Objects][dto].

---

## Your Task

Add some attributes to our DTOs. Put:

- `Required` on everything but `Address` and `Phone`
- `EmailAddress` where it's appropriate
- `Range` on `Age` and limit it to the [0, 100] interval

[facade]: https://en.wikipedia.org/wiki/Facade_pattern
[dto]: https://en.wikipedia.org/wiki/Data_transfer_object
[RequiredAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.requiredattribute
[RangeAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.rangeattribute
[EmailAddressAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.emailaddressattribute
[RegularExpressionAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.regularexpressionattribute

[CodeTask](/resources/validation/dto.csharp.csx)
