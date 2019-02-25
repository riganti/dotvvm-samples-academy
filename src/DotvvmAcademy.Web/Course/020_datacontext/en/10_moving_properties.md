---
Title: Moving properties
CodeTask:
    Path: 10_moving_properties.csharp.csx
    Correct: ProfileDetailViewModel_10.cs
    Default: ProfileDetailViewModel_20.cs
---

# Moving properties

Hello and, again, welcome to DotVVM Academy. Let's make something similar to a 'Edit profile' page! First we need to clean up this ViewModel.

__DataContext__ or also __Binding Context__ is the target of bindings, the object whose members you bind to. You can change it to shorten your bindings and hide parts of the View.

---

## Tasks

- Move the `FirstName` and `LastName` property to the `Profile` class.
- Create a property of type `Profile` also called `Profile` in the ViewModel.