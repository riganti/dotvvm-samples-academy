# ViewModel Lifecycle

We want to use the facade to fill the `Items` collection, preferably asynchrounously. That's where `DotvvmViewModelBase` comes in.

`DotvvmViewModelBase` allows you to override three methods that are often referred to as the ViewModel lifecycle:

- `public Task Init();` gets called just after the ViewModel is created. You are supposed to load the
initial data here.
- `public Task Load();` gets called after the ViewModel has been populated with data from the Client (even if there was no data from the Client).
- `public Task PreRender();` gets called after commands invoked by the Client have been executed.

---

## Your Task

Make `ToDoViewModel` inherit from `DotvvmViewModelBase` and override `PreRender` in order to load the `Items` collection asynchronously using `ToDoFacade.GetToDoItems()`.

> If we were to override `Init` or `Load`, changes our commands would make would not get displayed until the __next__ request.

[CodeTask](/resources/collections/viewmodel_lifecycle.csharp.csx)