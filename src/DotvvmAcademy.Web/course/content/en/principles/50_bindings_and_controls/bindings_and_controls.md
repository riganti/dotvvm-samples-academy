# Bindings and Controls

We continue our tour of dothtml's features.

## Bindings

[Bindings] are perhaps the most powerful feature of dothtml. They create a bond between the View and the ViewModel 
and they keep it up-to-date.


The most frequently used binding kind is the [Value Binding]:

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

Add these elements as children to the `<body>` element:

- `<div>` with its content bound to the `Result` property we created earlier
- two [`<dot:TextBox>`][textbox] controls with their `Text` property value-bound to `LeftOperand`
and `RightOperand` respectively

[bindings]: https://www.dotvvm.com/docs/tutorials/basics-binding-syntax
[value binding]: https://www.dotvvm.com/docs/tutorials/basics-value-binding
[controls]: https://www.dotvvm.com/docs/tutorials/basics-built-in-controls
[properties]: https://www.dotvvm.com/docs/tutorials/basics-control-properties-and-attributes
[textbox]: https://www.dotvvm.com/docs/controls/builtin/TextBox

[CodeTask](/resources/principles/view_controls.dothtml.csx)