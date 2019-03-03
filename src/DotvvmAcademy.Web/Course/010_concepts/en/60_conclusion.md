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

- Every page in DotVVM consists of the View and the ViewModel. We are using the MVVM design pattern.
- The Views define what the users can see and interact with.
- ViewModels keep the state of the page and handle user actions.
- DOTHTML extends plain HTML with directives, data-bindings expressions and controls.

> Note: If you wonder what's the missing part of MVVM, it's the model. By Model we mean the external services which provide the application logic and data (i.e. databases, payment gates, mailing routines and so on).