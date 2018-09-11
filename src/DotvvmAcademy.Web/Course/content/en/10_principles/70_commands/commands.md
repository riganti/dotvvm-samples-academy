---
Title: Commands
CodeTask: /resources/principles/view_commands.dothtml.csx
---

# Commands

[Commands] are a kind of binding that doesn't keep a value updated, but produces a [Postback],
request for the server to do something.

```dothtml
<dot:Button Text="Click me!"
            Click="{command: FooBar()}" />
```

Upon clicking the button, a [postback] is sent to the Server, ViewModel is instantiated, populated with data from the Client, and then `FooBar()` gets called.

---

## Your Task

Add four [`<dot:Button>`][button] controls to the `<body>` element that each:

- Have their `Click` property command-bound to a different arithmetic method, which we created earlier, in this order: `Add`, `Subtract`, `Multiply`, `Divide`
- Have their `Text` property set to an appropriate arithmetic operator (+, -, *, /)

[command]: https://www.dotvvm.com/docs/tutorials/basics-command-binding
[button]: https://www.dotvvm.com/docs/controls/businesspack/Button
[postback]: https://stackoverflow.com/questions/183254/what-is-a-postback
