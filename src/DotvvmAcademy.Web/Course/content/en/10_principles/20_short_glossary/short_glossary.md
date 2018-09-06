---
Title: Short Glossary
---

# Short Glossary

DotVVM is a [MVVM] web framework, but what does that even mean?

## Model-View-ViewModel

MVVM stands for Model-View-ViewModel. It's an architectural pattern that divides the objects in an application into three categories:

### Models

Models contain the data and business logic (e.g. database queries).

### Views

Views are what users see and interact with. In DotVVM they are written in [dothtml], our flavor of html.

### ViewModels

It is the job of ViewModels to interact with Models and reshape the data into a form that Views can display.

Views depend on ViewModels. ViewModels depend of Models. __Models have no idea about Views.__

## Server, Client, and User

In these lessons and in our [documentation], we make a distinction between the Server, the Client, and the User. 

- User is a person using our application
- Client is the User's browser sending requests to the Server
- Server is the computer our application is running on

Client communicates with the Server in order to display content to the User.

Since we are not going to need any models in our calculator, we'll start with the ViewModel instead.

[mvvm]: https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel
[dothtml]: https://www.dotvvm.com/docs/tutorials/basics-first-page
[documentation]: https://www.dotvvm.com/docs