---
Title: Loading Data
CodeTask: /resources/030_todolist/20_loading_data.csharp.csx
---

# Loading Data


Now, in a real application, you'd probably use the database to store the Todo items. We, however, will opt in for a simpler solution: bouncing a list of items between the Server and the Client. That means we have to instantiate the `List` only on the first request. How are we gonna do that, I hear?

We'll hook into DotVVM's lifecycle by overriding the `Init()` method of `DotvvmViewModelBase`. We can use the `Context.IsPostBack` property to check if the request is NOT first.

Finish the `Init()` method by initializing `Items` only if `Context.IsPostBack` is false.