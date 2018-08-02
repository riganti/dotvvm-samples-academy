# Commands

The second most frequently used kind of binding is the [Command Binding][command]. It's used like this:

```dothtml
<dot:Button Text="Click me!"
            Click="{command: FooBar()}" />
```

---

Add four [`<dot:Button>`][button] controls each with its `Click` property command-bound to one of methods we created
earlier. Also set their `Text` property to their respective arithmetic operator (+, -, *, /).

[command]: https://www.dotvvm.com/docs/tutorials/basics-command-binding
[button]: https://www.dotvvm.com/docs/controls/businesspack/Button

[CodeTask]("/resources/principles/view_commands.dothtml.csx")