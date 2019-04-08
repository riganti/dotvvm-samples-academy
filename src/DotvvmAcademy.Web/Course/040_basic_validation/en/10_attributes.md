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

__Attributes__ from the `System.ComponentModel.DataAnnotations` namespace represent the easiest way to validate something in DotVVM.

---

## Tasks

- Add `[Required]` to the `Email` and `Password` properties.
- Add `[EmailAddress]` to the `Email` property.