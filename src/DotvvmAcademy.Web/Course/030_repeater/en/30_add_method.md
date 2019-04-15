---
Title: Add Method
Moniker: add-method
CodeTask:
    Path: 30_add_remove.csharp.csx
    Default: ToDoViewModel_30.cs
    Correct: ToDoViewModel_40.cs

---

# Add Method

We want to be able to add and remove items from the collection.

In the next step, we'll add a `TextBox` and a `Button` to let the user add new To-Do items.

```dothtml
<dot:TextBox Text="{value: NewItem}" />
<dot:Button Click="{command: Add()}"
            Text="Add Item" />
```

---

## Tasks

Let's write the adding logic first.

- Add a `string` property called `NewItem`.
- Inside the `Add` method, create a new `ToDoItem` and initialize its `Text` property to `NewItem`. Then add the instance to `Items`.
