---
Title: More ModelState
CodeTask: 70_more_modelstate.csharp.csx
---

Finally, we need to create errors when registration goes awry.

By using the `Validation.Target` property on the buttons, we've lost the convenience provided by the `AddModelError` as it depends on the target being `LogInRegistrationViewModel`. We no longer need to set any paths to validated properties as the path is automatically set to the validation target for each Command.

Take a look at the `LogIn` Command. It has already been fixed. Fix the `Register` Command. Create an error when the password's length is less than 8 or registration fails.