---
Title: Add & Remove
Moniker: add-remove
CodeTask:
    Path: 30_add_remove.csharp.csx
    Default: ToDoViewModel_30.cs
    Correct: ToDoViewModel_40.cs

---

# Add & Remove

We want to be able to add and remove items from the `Items` collection.

---

## Tasks

- Add a `string` property called `NewItem`.
- Inside the `Add` method, append `NewItem` to `Items`.
- Inside the `Remove` method, delete the `item` parameter from `Items`.