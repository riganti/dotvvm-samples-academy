---
Title: The View
Moniker: view
CodeTask:
    Path: 20_view.dothtml.csx
    Default: Counter_10.dothtml
    Correct: Counter_20.dothtml
    Dependencies:
        - CounterViewModel_20.cs

---

# The View

__Views__ are everything users can see and interact with. In DotVVM, views are written in __DotHTML__, a flavour of _HTML_.

You can see two obvious differences from plain HTML:

- the line starting with `@viewModel` is the __ViewModel Directive__. It tells DotVVM which ViewModel belongs to the page, and

- __value binding__ expressions that bind ViewModel properties to specific places in the View:

```dothtml
<p>{{value: Result}}</p>
```

When the user loads the page, the value of the `Result` property will displayed inside the `<p>` element. Whenever the value of the property in the viewmodel changes, the text in the page gets updated.

---

## Tasks

- Add a `<p>` element inside the `<body>`.
- Use a value binding to display value of the `Result` property inside of the `<p>` element.
