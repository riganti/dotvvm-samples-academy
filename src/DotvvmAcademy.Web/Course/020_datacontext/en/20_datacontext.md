---
Title: DataContext
CodeTask: 20_datacontext.dothtml.csx
---

# Explicit DataContext

Every control has a reference to its DataContext that it inherited from its parent. By default it's the ViewModel, but you can set it explicitly using a binding:

```dothtml
<div DataContext="{value: Profile}">
```

---

## Tasks

- Value-bind the `DataContext` property of the `<div>` element to the `Profile` property.
- Edit the bindings inside the `<div>` element to target their new DataContext.
