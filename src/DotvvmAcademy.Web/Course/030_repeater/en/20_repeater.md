---
Title: Repeater
CodeTask: 20_repeater.dothtml.csx
---

# Repeater

Repeater is a control that renders a collection of items inside its `DataSource` property using an item template:

```dothtml
<dot:Repeater DataSource="{value: Items}">
    <p>{{value: _this}}</p>
</dot:Repeater>
```

A `<p>` element will be rendered for every item in `Items`. DataContext changes inside the Item template to the item object itself. We can refer to the item using the `_this` pseudo-variable.

---

## Tasks

- Add a `Repeater` to the `<body>` element with `DataSource` bound to `Items`.
- Render a `<p>` element for every item in the `Items` collection.
- Bind the item to the element using `_this`.