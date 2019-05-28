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

By default the `DataContext` property of all controls is set to the ViewModel itself. You can set a different `DataContext` to any control and it will also be applied to all children of the control.

As you can see, the data-binding expression references the `FirstName` property which can be found in the `Profile` object, not in the ViewModel itself.

```dothtml
<div DataContext="{value: Profile}">
    <dot:TextBox Text="{value: FirstName}"/>
</div>
```

---

## Tasks

- Value-bind the `DataContext` property of the `<div>` element to the `Profile` property of the ViewModel.
- Modify the binding expressions inside the `<div>` element to respect their new `DataContext`.
