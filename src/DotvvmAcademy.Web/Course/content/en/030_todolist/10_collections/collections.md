---
Title: Collections in a ViewModel
---

# Collections

Hello! This time, we'll look into using collections in ViewModels by building a To-Do List. Are you up for it?

ViewModel can contain any sort of collection that can be instantied. That means Arrays, Lists, HashSets and Dictionaries are okay. While IEnumerables, ILists and IDictionaries aren't.

> Um, actually, you can use them as well if you don't expect them to return from the Client by adding the `Bind` attribute.

Add a property of type `List<string>` called `Items`.