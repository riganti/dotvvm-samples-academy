---
Title: Life-cycle
Moniker: life-cycle
CodeTask: 
    Path: 50_lifecycle.csharp.csx
    Default: ToDoViewModel_50.cs
    Correct: ToDoViewModel_60.cs
---

# Life-cycle

ViewModels inheriting from `DotvvmViewModelBase` can hook into its life-cycle. We'll override the `Load` method and initialize the `Items` collection only if it is `null`.

> Note: We can't initialize it right away because we'd overwrite the old collection that gets sent from the user during the `Add` and `Remove` commands.

---

## Tasks

- Initialize the `Items` collection inside the `Load` method only if it's `null`.

> Note: Learn more about the life-cycle in the [docs](https://www.dotvvm.com/docs/tutorials/basics-viewmodels/latest).