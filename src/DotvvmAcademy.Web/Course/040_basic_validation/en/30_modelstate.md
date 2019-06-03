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

Validation occurs before any commands are invoked. You can also create custom validation errors in the command with the `AddModelError` extension method:

```csharp
this.AddModelError(vm => vm.ValidatedProperty, "An error.");
Context.FailOnInvalidModelState();
```

The lambda expression identifies the property that is in an invalid state. The `FailOnInvalidModelState` call will interrupt the request if there are any errors. These will be displayed to the user.

---

## Tasks

- Inside the `LogIn` method:
    - Call `accountService.LogIn(Email, Password)`. This method returns a `bool`.
    - If the previous call returns `false`, report a validation error using `this.AddModelError`.
    - Use `vm => vm.Email` as the validated property identifier.