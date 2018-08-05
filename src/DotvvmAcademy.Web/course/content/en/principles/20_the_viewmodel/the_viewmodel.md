# The ViewModel

DotVVM is said to use the [MVVM] pattern, but what does that even mean? MVVM stands for Model-View-ViewModel,
which means that the app is divided into 3 kinds of objects:

## Models

Contain the Data and Business Logic of your application. They handle database connections, payment gates, etc.

## Views

They are what users see and interact with; the part written in [dothtml], our flavor of html.

## ViewModels

It is their job to interact with Models and reshape the data into a form that Views can display.

Views depend on ViewModels. ViewModels depend of Models. What the users see on their screens doesn't concern the Models.

Since we won't need any models for our calculator, we'll start directly with the ViewModel instead.

---

Declare an empty class named `CalculatorViewModel` in the `DotvvmAcademy.Course` workspace.

[mvvm]: https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel
[dothtml]: https://www.dotvvm.com/docs/tutorials/basics-first-page

[CodeTask](/resources/principles/viewmodel_stub.csharp.csx)