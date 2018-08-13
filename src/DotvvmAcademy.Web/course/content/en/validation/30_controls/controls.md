# Controls

Of course, there are things in [Dothtml] that make validation easier.

## General behavior

__Validation occurs only on postbacks.__

There are two attached properties you can set on any control to change the behavior of validation in its subtree:

- `Validation.Enabled` - any postback coming from the subtree either triggers validation or doesn't
- [`Validation.Target`][target] - use this if you need to validate only a part of the ViewModel on a postback

## Validator

[`Validator`][validator] is a control that enables you to define what to display if a property is not valid.

Consider the following example: I want to show a generic error message if something is wrong with the `Text` property of a hypothetical ViewModel.

```dothtml
<dot:Validator Value="{value: Text}"
               InvalidCssClass="error">
    Something went horribly wrong!
<dot:Validator>
<dot:TextBox Text="{value: Text}" />
```

Or if I wanted to show the error message from the server instead, I would do it like this:

```dothtml
<dot:Validator Value="{value: Text}"
               InvalidCssClass="error"
               ShowErrorMessageText="true" />
<dot:TextBox Text="{value: Text}" />
```

Since all Validator properties are so-called attached properties as well, you can set them directly on the TextBox and they will work just the same:

```dothtml
<dot:TextBox Text="{value: Text}"
             Validator.Value="{value: Text}"
             Validator.InvalidCssClass="error"
             Validator.SetToolTipText="true"/>
```

Validator properties are applied to the control's whole subtree just like Validation properties.

## ValidationSummary

[`ValidationSummary`][validationsummary] displays validation errors from a [`Validation.Target`][target] (the whole ViewModel by default).

Let's say I have a property called `DTO` that I'm trying to validate and I want to display errors from its properties, its properties' properties, and itself:

```
<dot:ValidationSummary Validation.Target="{value: DTO}"
                       IncludeErrorsFromChildren="true"
                       IncludeErrorsFromTarget="true" />
```

By default only top-level properties of the [`Validation.Target`][target] get validated. `IncludeErrorsFromChildren` triggers recursion and `IncludeErrorsFromTarget` adds errors attached to the target itself.

---

## Your Task

- Set `Validation.Target` on the Login form to the `Login` property
- Set `Validation.Target` on the Registration form to the `Registration` property
- Add `<dot:Validator>` controls before the TextBoxes in the Log In form, bind them to their respective values
and let them show error messages
- Add `<dot:ValidationSummary>` at the end of the Register form. Display errors from the `Registration` property in it.

[target]: https://www.dotvvm.com/docs/tutorials/basics-validation-target
[validator]: https://www.dotvvm.com/docs/controls/builtin/Validator
[validationsummary]: https://www.dotvvm.com/docs/controls/builtin/ValidationSummary
[controls]: https://www.dotvvm.com/docs/tutorials/basics-validator-controls
[dothtml]: https://www.dotvvm.com/docs/tutorials/basics-first-page

[CodeTask](/resources/validation/view.dothtml.csx)