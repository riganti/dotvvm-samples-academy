---
Title: Collections
CodeTask: 10_collections.csharp.csx
---

# Collections

Hello. DataContext can change implicitly in controls like _Repeaters_. We'll illustrate that by making a To-Do list.

---

## Tasks

- Add a property of type `List<string>` called `Items` to the ViewModel.

> Note: A ViewModel can contain any sort of collection that can be deserialized. That means arrays, `Lists`, `HashSets` and `Dictionaries` are okay, while `IEnumerables`, `ILists` and `IDictionaries` are not.