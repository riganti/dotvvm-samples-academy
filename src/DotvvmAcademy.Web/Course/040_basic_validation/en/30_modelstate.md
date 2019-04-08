---
Title: ModelState
Moniker: modelstate
CodeTask:
    Path: 30_modelstate.csharp.csx
    Default: LogInViewModel_20.cs
    Correct: LogInViewModel_30.cs
    Dependencies: 
        - .solution/LogIn/AccountService.cs
---

# ModelState

Validation occurs when commands are invoked. You can create custom validation errors with the `AddModelError` extension method:

```csharp
this.AddModelError(vm => vm.ValidatedProperty, "An error.");
Context.FailOnInvalidModelState();
```

The lambda identifies the property that is in an invalid state and the `FailOnInvalidModelState` call is required for errors to be sent to the user.

---

## Tasks

- Inside the `LogIn` method:
    - Call `accountService.LogIn(Email, Password)`. This method returns a `bool`.
    - If the previous call returns `false`, report a validation error using `this.AddModelError`.
    - Use `vm => vm.Email` as the validated property identifier.