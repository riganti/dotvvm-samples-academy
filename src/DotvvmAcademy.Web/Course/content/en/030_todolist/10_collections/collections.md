---
Title: Collections in a ViewModel
CodeTask: /resources/030_todolist/10_collections.csharp.csx
---

# Collections

Hello! This time, we are building a To-Do list and learning about collections in ViewModels.

ViewModel can contain any sort of collection that can be instantiated. That means arrays, Lists, HashSets and Dictionaries are okay, while IEnumerables, ILists and IDictionaries are not.

> Um, actually, you can use `IEnumerables` and other interfaces as well if you use the `Bind` attribute to specify that they should only be sent to the Client and not back.

Add a property of type `List<string>` called `Items`.