---
Title: Moving Properties
Moniker: moving-properties
CodeTask:
    Path: 10_moving_properties.csharp.csx
    Default: ProfileDetailViewModel_10.cs
    Correct: ProfileDetailViewModel_20.cs
---

# Moving Properties

Hello and, again, welcome to DotVVM Academy. Let's make an 'Edit profile' page! First we need to clean up this ViewModel.

__Binding Context__ (the `DataContext` property) is the target of bindings, the object whose members are used in the expression. You can change the Binding Context to shorten your bindings and also hide specific parts of the View.

Often it is convenient to have more complex objects in the ViewModel. Clever use of Binding Context can help to better structure the View.

---

## Tasks

- Move the `FirstName` and `LastName` property to the `Profile` class.
- Create a property of type `Profile` in the ViewModel. Name it `Profile`.
