---
Title: ModelState
CodeTask: /resources/040_login_registration/30_modelstate.csharp.csx
---

# ModelState

Validation happens on PostBacks. PostBacks happen when Commands are invoked. Commands are invoked for instance when a `Button` gets pressed.

Sometimes validation prevents a PostBack from ever reaching the Server. This happens for example when a property is null while bearing the `RequiredAttribute`. More complicated errors, however, have to be determined on the Server. In our case, we need to create an error when the `LogIn` Command is unsuccessful. You can create validation errors manually with the `AddModelError` extension method:

```csharp
this.AddModelError(vm => vm.ValidatedProperty, "Something terrible has happened.");
Context.FailOnInvalidModelState();
```

Notice the `FailOnInvalidModelState` call. If you ever have problems with validation, check that this call is present.

The `LogIn` method of `AccountService` returns a boolean indicating its success. Change the `LogIn` Command to create an error when log-in fails using the `AddModelError` extension. Put the `LogInForm` property in place of `ValidatedProperty`.