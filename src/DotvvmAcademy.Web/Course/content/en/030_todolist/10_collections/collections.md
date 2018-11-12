---
Title: Collections in a ViewModel
CodeTask: /resources/030_todolist/10_collections.csharp.csx
---

# Collections

Hello! Collections are useful. To-do lists contain collections. Let's make one.

A ViewModel can contain any sort of collection that can be deserialized. That means arrays, `Lists`, `HashSets` and `Dictionaries` are okay, while `IEnumerables`, `ILists` and `IDictionaries` are not.

Add a property of type `List<string>` called `Items` to the `ToDoViewModel`.