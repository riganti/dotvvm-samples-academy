---
Title: Validator
Moniker: validator
CodeTask:
    Path: 20_validator.dothtml.csx
    Default: LogIn_10.dothtml
    Correct: LogIn_20.dothtml
    Dependencies:
        - LogInViewModel_20.cs
        - .solution/LogIn/AccountService.cs
---

# Validator

In the View, we can use the `Validator` control to show validation messages from the attributes:

```dothtml
<dot:Validator Value="{value: Email}"
               ShowErrorMessageText="true" />
```

---

## Tasks

- Add a `Validator` control next to each `TextBox`.
- Make sure to set `ShowErrorMessageText` to `true`.
- Bind their `Value` property to the same properties as their adjacent `TextBox` `Text` property.
