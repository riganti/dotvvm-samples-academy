---
Title: Attributes
Moniker: attributes
CodeTask:
    Path: 10_attributes.csharp.csx
    Default: LogInViewModel_10.cs
    Correct: LogInViewModel_20.cs
    Dependencies: 
        - .solution/LogIn/AccountService.cs
---

# Attributes

In this lesson, we'll validate a Log In form.

__Attributes__ from the `System.ComponentModel.DataAnnotations` namespace provide the easiest way to validate user inputs in DotVVM.

---

## Tasks

- Add the `[Required]` attribute to the `Email` and `Password` properties.
- Add the `[EmailAddress]` attribute to the `Email` property.