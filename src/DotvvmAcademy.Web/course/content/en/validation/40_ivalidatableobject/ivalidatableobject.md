# IValidatableObject

Last but definitely not least, you can gain finer-grained control over validation by using the `IValidatableObject`
interface.

It contains a single method, `IEnumerable<ValidationResult> Validate(ValidationContext)`.

It's probably easier to explain using an example:

```csharp
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Validation;

public class MyAmazingViewModel : DotvvmViewModelBase, IValidatableObject
{
    public string Secret { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (string.IsNullOrEmpty(Secret) || Secret.Length < 10)
        {
            yield return CreateValidationResult(v => v.Secret, "Your secret is too short!");
        }
    }
}
```

The `CreateValidationResult` is an extension method of the `IDotvvmViewModel` interface. The lambda it
takes as its first parameter is supposed to point to the property the error should be attached to.

Its might also be helpful to know that you can access the Validation.Target that applies to the current request in
the `Context.ModelState.ValidationTarget` property.

---

Make `FormViewModel` implement the `IValidatableObject`. If the ValidationTarget is the RegistrationDTO object,
Validate its `Password` property thusly:

- It has to be at least 16 characters long
- It has to contain a number, a dollar sign, and an underscore
- Send a different error message for each requirement above