---
Title: The ViewModel
CodeTask: /resources/010_calculator/10_viewmodel.csharp.csx
---

# The ViewModel

Hello and welcome to the first lesson of DotVVM Academy! To explore DotVVM's most important concepts, we're going to build a calculator.

DotVVM is a web framework following the Model-View-ViewModel (MVVM) software pattern, which divides objects into three groups: Models, Views, and ViewModels. Let's get familiar with the ViewModel group.

ViewModels are just plain old C# classes that contain public properties and methods. Our calculator is going to need three public integer properties named `Result`, `Number1`, and `Number2`. Add them to the `CalculatorViewModel` class. Don't forget to add both getters and setters!