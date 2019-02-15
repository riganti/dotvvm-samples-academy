---
Title: The View
CodeTask: 20_view.dothtml.csx
---

# The View

__Views__ are everything users see and interact with. ViewModels provide the data and logic for them. In DotVVM, they are written in __Dothtml__, a flavour of _HTML_.

A `@viewModel` __directive__ is at the beginning of every View. It tells DotVVM what ViewModel goes with it.

A __Value Binding__ is a bond between the View and the ViewModel. It looks like this:

```dothtml
<p>{{value: Result}}</p>
```

When user loads the page, value of the `Result` property will be inside the `<p>` element and will update itself automatically.

---

## Tasks

- Add a `<p>` inside the `<body>` element.
- Bind the inside of the `<p>` to the `Result` property.
