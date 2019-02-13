---
Title: Back to Root
CodeTask: 40_back_to_root.dothtml.csx
---

# Back to Root

Even inside a changed DataContext, you can access the ViewModel using the `_root` pseudo-variable:

```dothtml
<dot:Button Click="{command: _root.Create()}"
            Text="Create" />
```

---

## Tasks

- Add a `<dot:Button>` to the `<div>` that invokes the `Create` method.
- Add a `<dot:Button>` to the `<div>` that invokes the `Delete` method.