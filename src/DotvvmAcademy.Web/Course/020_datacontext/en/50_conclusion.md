---
Title: Conclusion
Moniker: conclusion
EmbeddedView:
    Path: .solution/ProfileDetail/Views/ProfileDetail.dothtml
    Dependencies:
        - .solution/ProfileDetail/ViewModels/ProfileDetailViewModel.cs
Archive:
    Path: .solution
---

# Conclusion

Good job! Now you know what `DataContext` is!

---

## Summary

- Binding Context is the default target of bindings.
- Every control has it inside its `DataContext` property.
- By default, controls inherit their `DataContext` from their parent.
- The `DataContext` of the document root is the ViewModel.
- If a `DataContext` of a control is set `null`, the control doesn't get rendered.
