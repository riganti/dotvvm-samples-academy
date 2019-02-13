---
Title: The View
CodeTask: 20_view.dothtml.csx
---

# The View

_Views_ are everything users see and interact with. In DotVVM, they are written in _Dothtml_, a flavour of _HTML_. At the beginning of every View is a `@viewModel` directive that tells DotVVM what ViewModel goes with it.

A _Value Binding_ is a bond between the View and the ViewModel. It looks like this:

```dothtml
<p>{{value: Result}}</p>
```

When user loads the page, `Result` will be inside the `<p>` element and will update itself automatically.

---

## Tasks

- Add a `<p>` inside the `<body>` element.
- Bind the inside of the `<p>` to the `Result` property.
