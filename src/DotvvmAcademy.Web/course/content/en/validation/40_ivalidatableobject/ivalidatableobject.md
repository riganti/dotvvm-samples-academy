# IValidatableObject

Last but definitely not least, you can gain finer-grained control over validation by using the [`IValidatableObject`][ivalidatableobject] interface.

It contains a single method, `IEnumerable<ValidationResult> Validate(ValidationContext);`.

It's probably easier to explain in an example:

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

The `CreateValidationResult` is an extension method of the `IDotvvmViewModel` interface, which `DotvvmViewModelBase` implements. The expression it takes as its first parameter is supposed to point to the property the error should be attached to.

You can access the `Validation.Target` that applies to the current request through the `Context.ModelState.ValidationTarget` property.

---

## Your Task

Make `FormViewModel` implement `IValidatableObject`. If the `Validation.Target` is the `RegistrationDTO` object,
validate its `Password` property thusly:

- It has to be at least 16 characters long
- It has to contain a number, a dollar sign, an underscore, and the word 'pumpkin'
- Send a different error message for each requirement above

> Note: This is of course just an example that should in no way be taken as a serious security advice.

[ivalidatableobject]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.ivalidatableobjec
[target]: https://www.dotvvm.com/docs/tutorials/basics-validation-target

[CodeTask](/resources/validation/viewmodel.csharp.csx)