---
Title: Remove Method
Moniker: remove-method
CodeTask:
    Path: 50_remove_method.csharp.csx
    Default: ToDoViewModel_50.cs
    Correct: ToDoViewModel_60.cs
---

# Remove Method

We'll allow the user to remove any item from the collection.

The `Remove` method needs to know the item to be deleted. We can pass it as a parameter.

```dothtml
<dot:Button Click="{command: _root.Remove(_this)}"
            Text="Remove Item" />
```

---

## Tasks

- Inside the `Remove` method, delete the `item` parameter from `Items`.
