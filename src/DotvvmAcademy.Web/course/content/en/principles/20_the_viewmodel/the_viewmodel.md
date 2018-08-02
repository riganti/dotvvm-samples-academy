# The ViewModel

In this lesson we're going to build a simple calculator and for that you are gonna need a rudimentary understanding 
of the [MVVM pattern][mvvm].

MVVM stands for Model-View-ViewModel, which means that the app is divided into 3 kinds of objects:

## Models

Contain the Data and Business Logic of your application. They handle database connections, payment gates, etc.

## Views

That's the part users interact with, the part written in [dothtml], our flavored html.

## ViewModels

It is their job to interact with Models and reshape the data into such a form, that Views can display.

It is important to note that while ViewModels depend on Models and Views depend on ViewModels, hovewer,
ViewModels have no idea about Views and Models have don't even need to be build with MVVM in mind at all.

Since we won't need any models for our calculator, we'll start directly with the ViewModel instead.

---

Please declare an empty class named `CalculatorViewModel` in the `DotvvmAcademy.Course` workspace.

[mvvm]: https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel
[dothtml]: https://www.dotvvm.com/docs/tutorials/basics-first-page

[CodeTask]("/resources/principles/viewmodel_stub.csharp.csx")