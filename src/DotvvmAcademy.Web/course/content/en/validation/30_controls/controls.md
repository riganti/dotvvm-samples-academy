# Controls

## General behavior

__Validation occurs only on postbacks.__

There are two attached properties you can set on any control to change the behavior of validation in
its subtree:

- Validation.Enabled - any postback coming from the subtree either triggers validation or doesn't
- Validation.Target - the part of the ViewModel whose validation gets triggered on postback

## Validator

[Validator] is a control that enables you to define what to display if a property is not valid. Consider the following
example. I want to show an error message with a CSS class next to a TextBox:

```dothtml
<dot:Validator Value="{value: Text}"
               InvalidCssClass="error"
               ShowErrorMessageText="">
</dot:Validator>
<dot:TextBox Text="{value: Text}" />
```

Since all Validator properties are attached properties, you can set them directly on the TextBox:

```dothtml
<dot:TextBox Text="{value: Text}"
             Validator.Value="{value: Text}"
             Validator.InvalidCssClass="error"
             Validator.SetToolTipText="true"/>
```

Validator properties are also inherited by the control's descendants.

## ValidationSummary

Displays ValidationErrors from a `Validation.Target`.

Let's say I have a property called `DTO` that I validate and I want to display errors from its properties,
its properties' properties and itself:

```
<dot:ValidationSummary Validation.Target="{value: DTO}"
                       IncludeErrorsFromChildren="true"
                       IncludeErrorsFromTarget="true" />
```

---

- Set `Validation.Target` on the Log In form to the `Login` property
- Set `Validation.Target` on the Register form to the `Registration` property
- Add `<dot:Validator>` controls before the TextBoxes in the Log In form, bind them to their respective values
and let them show error messages
- Add `<dot:ValidationSummary>` at the end of the Register form that shows errors from the `Registration` property