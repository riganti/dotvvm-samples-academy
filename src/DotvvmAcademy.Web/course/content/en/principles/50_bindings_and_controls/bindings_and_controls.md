# Bindings and Controls
 
[Bindings][binding] are perhaps the most powerful feature of dothtml. They allow you to create a relationship
between specific parts of the View and the ViewModel that updates itself automatically.
(DotVVM generates JavaScript snippets that do that.)

There are several kinds of bindings but for this step we'll make do with the [Value Binding][value binding].
It can be used inside elements like this:

```dothtml
<p>{{value: Text}}</p>
```

This inserts the value of the `Text` property into the `<p>` element and keeps it updated even if something changes the
value of `Text`.


[Controls][control] are reusable components that you can use with the `dot` prefix. They have bunch of
[DotVVM Properties][property], which allow bindings to be used like this:

```dothtml
<dot:TextBox Text="{value: Text}">
```
---

Add these elements as children to the `<body>` element:

- `<div>` with its content bound to the `Result` property we created earlier,
- a [`<dot:TextBox>`][textbox] with its `Text` property bound to `LeftOperand`, and
- the same thing but with `RightOperand`.

[binding]: https://www.dotvvm.com/docs/tutorials/basics-binding-syntax
[value binding]: https://www.dotvvm.com/docs/tutorials/basics-value-binding
[control]: https://www.dotvvm.com/docs/tutorials/basics-built-in-controls
[property]: https://www.dotvvm.com/docs/tutorials/basics-control-properties-and-attributes
[textbox]: https://www.dotvvm.com/docs/controls/builtin/TextBox

[CodeTask]("/resources/principles/view_controls.dothtml.csx")