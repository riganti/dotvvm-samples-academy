---
Title: Conclusion
EmbeddedView:
    Path: .solution/Counter/Views/Counter.dothtml
    Dependencies:
        - .solution/Counter/ViewModels/CounterViewModel.cs
Solution: .solution
---

# Conclusion

Congratulations! You have learned the basic concepts of DotVVM!

---

## Summary

- Every page in DotVVM consists of a View and a ViewModel. We are using the MVVM design pattern.
- Views define what the users can see and interact with.
- ViewModels keep the state of the page and handle user actions.
- DotHTML extends plain HTML with directives, binding expressions and controls.

> Note: If you wonder what's the missing part of MVVM, it's the model. By Model we mean the external services which provide the application logic and data (e.g. databases, mailing routines, payment gateways).