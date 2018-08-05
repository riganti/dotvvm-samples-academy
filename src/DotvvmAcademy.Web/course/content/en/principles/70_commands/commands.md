# Commands


A [Command] is another kind of binding. It's used like this:

```dothtml
<dot:Button Text="Click me!"
            Click="{command: FooBar()}" />
```

Upon clicking the button, a [postback] is sent to the server (the C# side), ViewModel is instantiated,
populated with data from the client (the JavaScript side) and then `FooBar()` gets called.

---

Add four [`<dot:Button>`][button] controls to the `<body>` element that:

- Have their `Click` property command-bound to one of methods we created earlier
- Have their `Text` property set to their respective arithmetic operator (+, -, *, /)

[command]: https://www.dotvvm.com/docs/tutorials/basics-command-binding
[button]: https://www.dotvvm.com/docs/controls/businesspack/Button
[postback]: https://stackoverflow.com/questions/183254/what-is-a-postback

[CodeTask](/resources/principles/view_commands.dothtml.csx)