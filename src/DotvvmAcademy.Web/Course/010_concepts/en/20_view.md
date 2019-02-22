---
Title: The View
CodeTask:
    Path: 20_view.dothtml.csx
    Default: Counter_10.dothtml
    Correct: Counter_20.dothtml
    Dependencies:
        - CounterViewModel_20.cs

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
