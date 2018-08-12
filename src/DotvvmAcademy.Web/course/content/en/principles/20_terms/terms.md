# Terms

DotVVM is a [MVVM] web framework, but what does that even mean?

## Model-View-ViewModel

MVVM stands for Model-View-ViewModel. It's an architectural pattern that divides the objects in an application into three categories:

### Models

Models contain the data and business logic. In most applications they handle database access.

### Views

Views are what users see and interact with. In DotVVM they are written written in [dothtml], our flavor of html.

### ViewModels

It is their job to interact with Models and reshape the data into a form that Views can display.

Views depend on ViewModels. ViewModels depend of Models. __Models have no idea about Views.__

## Server, Client, and User

In these lessons and in our [documentation], we make a distinction between the Server, the Client, and the User. 

- User is a person using our application
- Client is the User's browser / User's computer
- Server is the computer where our application is running

Client connects to the Server in order to display content to the User.

Since we won't need any models in our calculator, we'll start with the ViewModel instead.

---

## Your Task

Declare an empty class named `CalculatorViewModel` in the `DotvvmAcademy.Course.Calculator` namespace.

[mvvm]: https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel
[dothtml]: https://www.dotvvm.com/docs/tutorials/basics-first-page
[documentation]: https://www.dotvvm.com/docs

[CodeTask](/resources/principles/viewmodel_stub.csharp.csx)