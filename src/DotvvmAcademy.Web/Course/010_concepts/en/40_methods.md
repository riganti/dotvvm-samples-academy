---
Title: Methods
Moniker: methods
CodeTask:
    Path: 40_methods.csharp.csx
    Default: CounterViewModel_30.cs
    Correct: CounterViewModel_40.cs
---

# Methods

The __methods__ in the viewmodel can be invoked from the view. We'll use them to implement the add / subtract functionality.

> Note: DotVVM will make an AJAX request to the server, where the method in the viewmodel will be executed and the changes made to the viewmodel will be applied to the page.

---

## Tasks

- Inside the `Add` method, add the value of `Difference` to the `Result` property.
- Inside the `Subtract` method, subtract the value of `Difference` from the `Result` property.