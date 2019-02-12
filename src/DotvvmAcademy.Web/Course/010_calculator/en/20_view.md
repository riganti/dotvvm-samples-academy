---
Title: The View
CodeTask: 20_view.dothtml.csx
---

# The View

Views are everything users see and interact with. They shouldn't, however, be concerned with anything else. It is the job of ViewModels to provide data and logic for the Views.

DotVVM's Views are written in Dothtml, a flavour of HTML. At the beginning of every View is a `@viewModel` directive that tells DotVVM what ViewModel goes with it. 

Dothtml has a mechanism for creating a bond between the View and the ViewModel called the Value Binding. Creating a bond between the content of a paragraph and a `Text` property in the ViewModel is done like this:

```dothtml
<p>{{value: Text}}</p>
```

Let's start by displaying the `Result` of a calculation. Add a `<div>` inside the `<body>` element. Inside the `<div>` write a Value Binding to the `Result` property, we created earlier.
