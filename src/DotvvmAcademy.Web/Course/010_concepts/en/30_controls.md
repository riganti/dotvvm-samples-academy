---
Title: Controls
Moniker: controls
CodeTask:
    Path: 30_controls.dothtml.csx
    Default: Counter_20.dothtml
    Correct: Counter_30.dothtml
---

# Controls

DotVVM also adds the concept of __controls__, reusable components that can be added anywhere in the view. You can easily recognize them by the `<dot:` prefix.

```dothtml
<dot:TextBox Text="{value: Difference}" />
```

The `TextBox` control is essentially an `<input type="text">` element that supports binding expressions.

---

## Tasks

- Add a `<dot:TextBox>` to the `<body>` element.
- Use a value binding to bind the `Difference` property to the `Text` attribute of the `TextBox`.
