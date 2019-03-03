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

__Views__ are everything the users can see and interact with. In DotVVM, the Views are written in __DOTHTML__, a flavour of _HTML_.

You can see two enhancements to the plain HTML:

* The line starting with `@viewModel` __directive__ tells DotVVM which ViewModel belongs to the page.

* __Value Binding__ expressions bind ViewModel properties to specific places in the View:

```dothtml
<p>{{value: Result}}</p>
```

When the user loads the page, the value of the `Result` property will by printed out inside the `<p>` element. And whenever the value of the property in the ViewModel changes, the text in the page will be updated.

---

## Tasks

- Add a `<p>` element inside the `<body>`.
- Bind the content of the `<p>` element to the `Result` property of the ViewModel.



