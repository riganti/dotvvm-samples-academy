---
Title: Forms
CodeTask: /resources/030_todolist/50_add_form.dothtml.csx
---

# Forms

We have the commands, we need the forms.

- Add a `TextBox` to `<body>`. Value-bind it to `NewItem`.
- Add a `Button` to `<body>`. Command-bind it to `Add`.
- Add a `Button` to the `Repeater`'s item template. This button needs to call `Remove` on the ViewModel.

> Hint: You can use `_this` to refer to the Todo item and `_root` to refer to the ViewModel.