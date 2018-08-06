# ViewModel Lifecycle

We want to use the facade to fill the `Items` collection, preferably asynchrounously.
That's where `DotvvmViewModelBase` comes in.

`DotvvmViewModelBase` allows you to override three methods, that are often refered to as the ViewModel lifecycle:

- `public Task Init();` gets called just after the ViewModel is created. You are supposed to load the
initial data here.
- `public Task Load();` gets called after the ViewModel has been populated with data from the client (even if there
wasn't any).
- `public Task PreRender();` gets called after commands issued by the client have been executed

---

Make `ToDoViewModel` inherit from `DotvvmViewModelBase` and override `PreRender` in order to load the `Items` collection
asynchronously.

Note: We override `PreRender` because our item-adding and item-removing commands will change the contents of `Items`.
If we were to override `Init` or `Load`, changes we made would not get displayed until the __next__ request.