---
Title: ValidationSummary
CodeTask: /resources/040_login_registration/50_validationsummary.dothtml.csx
---

# ValidationSummary

ValidationSummary is an error aggregation control. It can display errors from the whole ViewModel or a specific part of it. You can influence this behavior by setting the `Validation.Target` attached property.

By default it includes errors only from the validation target's properties but not the target itself, that's when `IncludeErrorsFromTarget` comes to play.

Add a `ValidationSummary` next to the Register `Button`. Bind its `Validation.Target` property to `RegistrationForm`. Also set its `IncludeErrorsFromTarget` to true.