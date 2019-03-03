---
Title: Controls
CodeTask:
    Path: 30_controls.dothtml.csx
    Default: Counter_20.dothtml
    Correct: Counter_30.dothtml
---

# Controls

DotVVM also add the concept of __Controls__, reusable components that can be added anywhere in the HTML. You can recognize them easily by the `<dot:` prefix.

```dothtml
<dot:TextBox Text="{value: Difference}" />
```

The `TextBox` control is essentially an `<input type="text">` element that supports data-bindings.

> Note: Technically, every standard HTML element is also a control in DotVVM, specifically _HtmlGenericControl_.

---

## Tasks

- Add a `<dot:TextBox>` control in the `<body>` element.
- Use value binding to bind its `Text` to the `Difference` property.
