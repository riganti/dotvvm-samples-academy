---
Title: Conclusion
Moniker: conclusion
EmbeddedView:
    Path: .solution/LogIn/Views/LogIn.dothtml
    Dependencies:
        - .solution/LogIn/ViewModels/LogInViewModel.cs
        - .solution/LogIn/AccountService.cs
Archive:
    Path: .solution
---

# Conclusion

Good work! Try the interactive sample to see validation in action.

> Note: The only valid credentials are "john@example.com" and "CorrectHorseBatteryStaple".

---

## Summary

- `DataAnnotation` attributes are the simplest form of validation.
- The Validator control can be used to show validation messages.
- Custom errors can be created during commands using the `AddModelError` extension method.

> Note: There are other validation concepts like [IValidatableObject](https://www.dotvvm.com/docs/tutorials/basics-validation/latest), the [ValidationSummary](https://www.dotvvm.com/docs/tutorials/basics-validator-controls/latest) control, and the [Validation.Target](https://www.dotvvm.com/docs/tutorials/basics-validation-target/latest) attached property.