# Conclusion

- `DataAnnotation` attributes are easy to use but cannot be customised
- `Validation.Target` is used to separate multiple validation areas
- `Validator` and `ValidationSummary` are used to display the errors
- `IValidatableObject` allows you to create a custom validation process
- You can also add errors to ModelState during commands, which is helpful since they can be
async while the `IValidableObject` interface cannot

Validation can be painful. I hope this helped.