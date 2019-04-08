---
Title: Conclusion
EmbeddedView:
    Path: .solution/ToDoList/Views/ToDo.dothtml
    Dependencies:
        - .solution/ToDoList/ViewModels/ToDoViewModel.cs
Archive:
    Path: .solution
---

# Conclusion

Congratulations! You've got yourself a functional To-Do list.

---

## Summary

- `Repeater` is dothtml's equivalent of a `foreach` loop.
- DataContext changes implicitly to an item inside the item template.
- Some property values get sent over from the user, these must be initialized carefully.