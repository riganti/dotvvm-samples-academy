---
Title: Bindings and Controls
CodeTask: /resources/principles/view_controls.dothtml.csx
---

# Bindings and Controls

We continue our tour of [Dothtml]'s features.

## Bindings

[Bindings] are perhaps the most powerful feature of dothtml. They create a bond between the View and the ViewModel and they keep it up-to-date when the ViewModel changes.


The most frequently used kind of bindings is the [Value Binding].

```dothtml
<p>{{value: Text}}</p>
```

This inserts the value of the `Text` property into the `<p>` element and keeps it updated even if `Text` changes.


## Controls

[Controls] are reusable components accessible with the `dot` prefix. They have [properties] which can be bound:

```dothtml
<dot:TextBox Text="{value: Text}">
```
---

## Your Task

Add children to the `<body>` element:

- a `<div>` element; bind its content to the `Result` property
- two [`<dot:TextBox>`][textbox] controls; bind their `Text` property to `LeftOperand`
and `RightOperand` respectively

[dothtml]: https://www.dotvvm.com/docs/tutorials/basics-first-page
[bindings]: https://www.dotvvm.com/docs/tutorials/basics-binding-syntax
[value binding]: https://www.dotvvm.com/docs/tutorials/basics-value-binding
[controls]: https://www.dotvvm.com/docs/tutorials/basics-built-in-controls
[properties]: https://www.dotvvm.com/docs/tutorials/basics-control-properties-and-attributes
[textbox]: https://www.dotvvm.com/docs/controls/builtin/TextBox