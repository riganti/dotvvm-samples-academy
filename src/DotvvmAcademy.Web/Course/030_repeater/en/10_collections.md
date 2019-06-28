---
Title: Collections
Moniker: collections
CodeTask:
    Path: 10_collections.csharp.csx
    Default: ToDoViewModel_10.cs
    Correct: ToDoViewModel_20.cs
---

# Collections

Binding Context can change implicitly in controls like `Repeater`. We'll illustrate that by making a To-Do list.

---

## Tasks

- Add a property of type `List<ToDoItem>` called `Items` to the ViewModel and initialize it.

> Note: A ViewModel can contain any concrete collection type like `List`, `HashSet`, and `Dictionary`. Using collection interfaces like `IEnumerable`, `IList`, and `IDictionary` is not recommended.
