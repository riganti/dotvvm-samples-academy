---
Title: Conclusion
Moniker: conclusion
EmbeddedView:
    Path: .solution/ProfileDetail/Views/ProfileDetail.dothtml
    Dependencies:
        - .solution/ProfileDetail/ViewModels/ProfileDetailViewModel.cs
        - .solution/ProfileDetail/ViewModels/Profile.cs
Archive: 
    Path: .solution
---

# Conclusion

Good job! Now you know what DataContext is!

---

## Summary

- DataContext is the default target of bindings.
- Every control has a `DataContext` property.
- Controls by default inherit their `DataContext` from their parent.
- If the DataContext is `null`, the element doesn't get rendered.