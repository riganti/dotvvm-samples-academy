---
Title: Moving Properties
Moniker: moving-properties
CodeTask:
    Path: 10_moving_properties.csharp.csx
    Default: ProfileDetailViewModel_10.cs
    Correct: ProfileDetailViewModel_20.cs
---

# Moving Properties

Hello and, again, welcome to DotVVM Academy. In this lesson, we'll make a simple "Edit profile" page!

When you create complex pages, it is convenient to have complex objects in the ViewModel. In order to keep the data-binding expression simple, you can change the __Binding Context__ for a specific part of the View. 

__Binding Context__ (configured using the `DataContext` property) works as the target of bindings - the object whose members are used in the expression. 

Clever use of Binding Context can help to better structure the View, make the binding expressions shorter and also show or hide specific parts of the View.

Let's start with moving the properties containing the user information into a separate class.

---

## Tasks

- Move the `FirstName` and `LastName` property to the `Profile` class.
- Create a property of type `Profile` in the ViewModel. Name it `Profile`.
