---
Title: Back to Root
Moniker: back-to-root
CodeTask:
    Path: 40_back_to_root.dothtml.csx
    Default: ProfileDetail_20.dothtml
    Correct: ProfileDetail_30.dothtml
    Dependencies:
        - ProfileDetailViewModel_40.cs
---

# Back to Root

We need buttons to call the `Load` and `Unload` methods.

Even inside a Binding Context that is deep within the hierarchy, you can access the ViewModel using the `_root` pseudo-variable:

```dothtml
<dot:Button Click="{command: _root.Unload()}"
            Text="Unload" />
```

> There is a number of other [binding pseudo-variables](https://www.dotvvm.com/docs/tutorials/basics-binding-context/latest) such as `_parent`, `_collection` or `_this`.

---

## Tasks

- Add a `<dot:Button>` _outside_ the `<div>` and command-bind its `Click` property to the `Load` method.
- Add a `<dot:Button>` that invokes the `Unload` method _inside_ the `<div>` element. Don't forget to use `_root` - the `Unload` method is declared in the ViewModel, not in the `Profile` class.
