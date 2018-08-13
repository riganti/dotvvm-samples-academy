# Attributes

Let's validate a Log In form and a Registration form that are next to each other in a single View.

This is the current state of our ViewModel:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Course
{
    public class FormViewModel : DotvvmViewModelBase
    {
        private readonly LoginFacade loginFacade;
        private readonly RegistrationFacade registrationFacade;

        public FormViewModel(LoginFacade loginFacade, RegistrationFacade registrationFacade)
        {
            this.loginFacade = loginFacade;
            this.registrationFacade = registrationFacade;
        }

        public LoginDTO Login { get; set; }

        public RegistrationDTO Registration { get; set; }

        public Task LogIn()
        {
            return loginFacade.LogIn(Login);
        }

        public Task Register()
        {
            return registrationFacade.Register(Registration);
        }
    }
}
```

There are two properties whose type is a [Data Transfer Object][dto] and two methods that call an external [Facade].

DotVVM recognizes attributes from the `System.ComponentModel.DataAnnotations` namespace. For instance the:


- [RequiredAttribute] - value must not be null or empty
- [RangeAttribute] - numeric value must be within certain range
- [EmailAddressAttribute] - string must be in the format of an email address
- [RegularExpressionAttribute] - string must match a regular expression


---

## Your Task

Add some attributes to our DTOs. Put:

- `Required` on everything but `Address` and `Phone`
- `EmailAddress` where it's obviously needed
- `Range` on `Age` and limit it to the [0, 100] interval


[facade]: https://en.wikipedia.org/wiki/Facade_pattern
[dto]: https://en.wikipedia.org/wiki/Data_transfer_object
[RequiredAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.requiredattribute
[RangeAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.rangeattribute
[EmailAddressAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.emailaddressattribute
[RegularExpressionAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.regularexpressionattribute

[CodeTask](/resources/validation/dto.csharp.csx)
