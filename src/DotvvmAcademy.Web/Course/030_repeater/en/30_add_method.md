---
Title: Add Method
Moniker: add-method
CodeTask:
    Path: 30_add_method.csharp.csx
    Default: ToDoViewModel_30.cs
    Correct: ToDoViewModel_40.cs

---

# Add Method

We want to be able to add items to and remove items from the collection.

In the next step, we'll add a `TextBox` and a `Button` to let the user add new To-Do items.

```dothtml
<dot:TextBox Text="{value: NewItem}" />
<dot:Button Click="{command: Add()}"
            Text="Add Item" />
```

However, we need to do something else first.

---

## Tasks

Let's write the adding logic first.

- Add a `string` property called `NewItem` to the ViewModel.
- Inside the `Add` method, create a new `ToDoItem` object and initialize its `Text` property to `NewItem`. Then add the instance to `Items`.
