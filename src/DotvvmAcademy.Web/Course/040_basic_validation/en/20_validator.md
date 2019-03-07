﻿---
Title: Validator
CodeTask:
    Path: 20_validator.dothtml.csx
    Default: LogIn_10.dothtml
    Correct: LogIn_20.dothtml
    Dependencies:
        - LogInViewModel_20.cs
        - .solution/LogIn/AccountService.cs
---

# Validator

The `Validator` control can be used to show validation messages generated by the attributes:

```dothtml
<dot:Validator Value="{value: Email}"
               ShowErrorMessageText="true" />
```

---

## Tasks

- Add a `Validator` control next to each `TextBox`.
- Bind their `Value` property to the same properties as their adjacent `TextBox's` `Text` property.