---
Title: Repeater
---

# Repeater

Repeater is a control that renders a collection of items using an item template. You can use it by following these three steps:

1. Bind the Repeater's DataSource to the collection. 
2. Set the item template by adding child controls to the Repeater. These will be rendered for each element in the collection.
3. DataContext changes inside the template to the current item; double-check your bindings.

Add a Repeater to the `<body>` element. It needs to render a `<p>` containing the Todo text for every item in `Items`.