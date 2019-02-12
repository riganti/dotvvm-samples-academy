---
Title: ValidationTarget
CodeTask: 60_validationtarget.dothtml.csx
---

# ValidationTarget

We've seen what this attached property does on `ValidationSummary` in the last step. It allows you to separate a View into sections that are validated separately.

On a `Button` (or a different control with Commands) it does the same. Right now if you were to press the Log-In or Register button the entire ViewModel would be validated. For instance, Log-In could fail because the Age property is set to an invalid value.

Fix the forms by binding the validation target on the button to `LogInForm` and `RegistrationForm` respectively.