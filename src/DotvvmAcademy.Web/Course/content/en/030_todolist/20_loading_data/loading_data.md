---
Title: Loading Data
---

# Loading Data

We're going to load the Todos from an external `IToDoService`. We can get an instance of this service through a technique called Constructor Dependency Injection. If you don't know what that is, imagine it as a magical being that always gives you what you want as long as it's a constructor parameter.

Where are we going to use it? We'll hook into DotVVM's lifecycle by overriding the `PreRender()` method of `DotvvmViewModelBase`. This method gets called after all commands have been invoked.

Finish the `PreRender()` method by calling `GetItems()` on the `toDoService` field. Assign the result to the `Items` property.