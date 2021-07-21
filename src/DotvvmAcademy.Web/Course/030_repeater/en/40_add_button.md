---
Title: Add Button
Moniker: add-button
CodeTask:
    Path: 40_add_button.dothtml.csx
    Default: ToDo_20.dothtml
    Correct: ToDo_30.dothtml
    Dependencies:
        - ToDoViewModel_40.cs
---

# Add Button

Now let's create the user interface for adding items.

---

## Tasks

- Add a `TextBox` to `<body>` and value-bind it to `NewItem`.
- Add a `Button` to `<body>` and command-bind it to `Add`.
