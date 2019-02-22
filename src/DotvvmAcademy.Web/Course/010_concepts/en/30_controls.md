---
Title: Controls
CodeTask:
    Path: 30_controls.dothtml.csx
    Default: Counter_20.dothtml
    Correct: Counter_30.dothtml
---

# Controls

__Controls__ are DotVVM's reusable components. You access them with the `dot` prefix.

```dothtml
<dot:TextBox Text="{value: Difference}" />
```
The `TextBox` control is essentially an `<input>` element that allows you to use bindings.

> Note: Technically, every standard HTML element is also a control, specifically an _HtmlGenericControl_.

---

## Tasks

- Add a `TextBox` control to the `<body>` element.
- Value-bind it to the `Difference` property.
