---
Title: Registration Attributes
CodeTask: 40_registration_attributes.csharp.csx
---

# Registration Attributes

The `RegistrationForm` class requires some `DataAnnotations` attributes as well.

- Add `Required` to all properties of `RegistrationForm`.
- Add `Range(18, double.MaxValue)` to `Age`.
- Add `EmailAddress` to `Email`.