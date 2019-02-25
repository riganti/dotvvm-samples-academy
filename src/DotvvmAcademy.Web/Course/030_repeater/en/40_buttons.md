---
Title: Buttons
CodeTask:
    Path: 40_buttons.dothtml.csx
    Default: ToDo_20.dothtml
    Correct: ToDo_30.dothtml
---

# Buttons

We also need some buttons to call the `Add` and `Remove` methods.

---

## Tasks

- Add a `TextBox` to `<body>` and value-bind it to `NewItem`.
- Add a `Button` to `<body>` and command-bind it to `Add`.
- Add a `Button` to the `Repeater`'s item template.
    - This button needs to call `Remove` on the ViewModel with `_this` as the parameter.

> Hint: You can use `_root` to refer to the ViewModel from inside the template.