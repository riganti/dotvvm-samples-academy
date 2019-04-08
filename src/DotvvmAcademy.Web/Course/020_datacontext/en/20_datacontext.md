---
Title: DataContext
Moniker: datacontext
CodeTask:
    Path: 20_datacontext.dothtml.csx
    Default: ProfileDetail_10.dothtml
    Correct: ProfileDetail_20.dothtml
    Dependencies:
        - ProfileDetailViewModel_20.cs
---

# DataContext

Every control has a DataContext that it __inherited__ from its parent in its `DataContext` property. By default it's the whole ViewModel, but you can set it explicitly using a binding:

```dothtml
<div DataContext="{value: Profile}">
```

---

## Tasks

- Value-bind the `DataContext` property of the `<div>` element to the `Profile` property.
- Modify the bindings inside the `<div>` element to respect their new DataContext.
