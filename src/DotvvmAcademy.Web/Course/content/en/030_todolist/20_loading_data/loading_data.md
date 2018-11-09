---
Title: Loading Data
CodeTask: /resources/030_todolist/20_loading_data.csharp.csx
---

# Loading Data

Now, in a real application, you'd probably use a database to store the todo items. We, however, will opt in for a simpler solution: bouncing a list of items between the Server and the Client. 

We need to instantiate the `List` only on the first request, so that we don't override the incoming data on postback. We'll hook into DotVVM's life-cycle by overriding the `Init()` method of `DotvvmViewModelBase`. We can use the `Context.IsPostBack` property to check if the request is NOT first.

Finish the `Init()` method by initializing `Items` only if `Context.IsPostBack` is false.