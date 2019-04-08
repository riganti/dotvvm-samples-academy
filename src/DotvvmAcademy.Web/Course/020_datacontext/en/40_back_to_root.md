---
Title: Back to Root
Moniker: back-to-root
CodeTask:
    Path: 40_back_to_root.dothtml.csx
    Default: ProfileDetail_20.dothtml
    Correct: ProfileDetail_30.dothtml
---

# Back to Root

We need buttons to call the `Create` and `Delete` methods.

Even inside a changed DataContext, you can access the ViewModel using the `_root` pseudo-variable:

```dothtml
<dot:Button Click="{command: _root.Create()}"
            Text="Create" />
```

---

## Tasks

- Add a `<dot:Button>` to the `<div>` that invokes the `Create` method.
- Add a `<dot:Button>` to the `<div>` that invokes the `Delete` method.