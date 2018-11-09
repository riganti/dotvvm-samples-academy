---
Title: Validator
CodeTask: /resources/040_login_registration/20_validator.dothtml.csx
---

# Validator

DataAnnotations attributes will automatically create an error that gets send to the client. To display this error to the user, you can use the `Validator` control or its attached properties.

To display the automatically created error message you'd to this:

```dothtml
<dot:Validator Value="{value: LogInForm.Email}"
               ShowErrorMessageText="true" />
```

Add a `Validator` control next to both `TextBox` controls. Bind their `Value` to the same properties as the respective `TextBox's` `Text`.
