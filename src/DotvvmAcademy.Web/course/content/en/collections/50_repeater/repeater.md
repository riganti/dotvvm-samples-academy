# Repeater

You can safely imagine the [`<dot:Repeater>`][repeater] control as a C# `foreach`. You bind a collection of items to it and you
give it an item template.

For instance, let's say you have a collection of strings called `Texts` and you want to generate
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

Create a dothtml stub and to the `<body>` element add a `<dot:Repeater>` with `DataSource` bound to the
`Items` collection. The Repeater has to display a `<div>` with the value of `ToDoItem.Text` inside of it
 for every `ToDoItem`.

[repeater]: https://www.dotvvm.com/docs/controls/builtin/Repeater
[datacontext]: https://www.dotvvm.com/docs/tutorials/basics-binding-context