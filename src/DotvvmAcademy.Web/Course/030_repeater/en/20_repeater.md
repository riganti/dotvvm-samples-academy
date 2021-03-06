﻿---
Title: Repeater
Moniker: repeater
CodeTask:
    Path: 20_repeater.dothtml.csx
    Default: ToDo_10.dothtml
    Correct: ToDo_20.dothtml
    Dependencies:
        - ToDoViewModel_20.cs
---

# Repeater

The `Repeater` control renders a collection of items from its `DataSource` property using a template:

```dothtml
<dot:Repeater DataSource="{value: Items}">
    <p>{{value: Text}}</p>
</dot:Repeater>
```

A `<p>` element will be rendered for every item in the `Items` collection. Inside the `Repeater`, the Binding Context changes to the item object itself.

---

## Tasks

Let's display the To-Do items.

- Inside the `<body>` element, add a `Repeater` with `DataSource` bound to `Items`.
- Display a `<p>` element for every item in the `Items` collection.
- Bind the paragraph content to the `Text` property.
