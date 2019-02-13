---
Title: Commands
CodeTask: 50_commands.dothtml.csx
---

# Commands

_Commands_ are a kind of bindings used to invoke ViewModel's methods. Commands can be used in a control like the `Button`:

```dothtml
<dot:Button Click="{command: Increment()}"
            Text="++" />
```
Changes made by the `Increment` method are sent back to the user.

---

## Tasks

- Add two `Button` controls to the `<body>` element.
- Command-bind them to the `Increment` and `Decrement` methods we created earlier.