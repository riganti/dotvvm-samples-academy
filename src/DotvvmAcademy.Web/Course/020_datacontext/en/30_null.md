---
Title: Null DataContext
Moniker: null-datacontext
CodeTask:
    Path: 30_null_datacontext.csharp.csx
    Default: ProfileDetailViewModel_30.cs
    Correct: ProfileDetailViewModel_40.cs
---

# Null DataContext

We haven't initialized the `Profile` property yet and so it's `null`. Because of that, the entire `<div>` and its contents won't be rendered. We can use this feature to hide parts of the page whenever we want.

---

## Tasks

Let's write some code to initialize the `Profile` property on demand.

- In the `Load` method, initialize the `Profile` property to a new instance of `Profile`.
- In the `Unload` method, set the `Profile` property to `null`.
