---
Title: DataContext
CodeTask: 20_datacontext.dothtml.csx
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
