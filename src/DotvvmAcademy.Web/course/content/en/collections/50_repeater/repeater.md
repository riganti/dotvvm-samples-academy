# Repeater

You can safely imagine the [`<dot:Repeater>`][repeater] control as a C# `foreach`. You bind a collection of items to it and you give it a template for the items.

For instance, let's say you have a collection of strings called `Texts` and you want to display
a paragraph for each of them:

```dothtml
<dot:Repeater DataSource="{value: Texts}">
    <p>{{value: this}}</p>
</dot:Repeater>
```

Inside the Repeater the [DataContext] changes. You no longer create bindings that link to the ViewModel itself.
Instead, your bindings link to strings inside the `Texts` collection. Therefore `{{value: this}}`
inserts the current string in the Repeater's "`foreach`" loop.

---

## Your Task

- Create a dothtml stub 
- Add a `<dot:Repeater>` control to the `<body>` element
  -  Bind its `DataSource` property to the`Items` collection
  -  Display a `<div>` with the value of `ToDoItem.Text` inside of it for every `ToDoItem`

[repeater]: https://www.dotvvm.com/docs/controls/builtin/Repeater
[datacontext]: https://www.dotvvm.com/docs/tutorials/basics-binding-context

[CodeTask](/resources/collections/view_stub.dothtml.csx)