---
Title: Commands
CodeTask: 50_commands.dothtml.csx
---

# Commands

Commands are just a different kind of binding. They invoke a function on the server. We call that a postback. Let's take a look at a sample. This is an excerpt from the ViewModel:

```csharp
public string Message { get; set; }

public void Foo()
{
    Message = "Bar";
}
```

Commands can be used by a control like the `Button`:
```dothtml
<dot:Button Click="{command: Foo()}" />
<dot:TextBox Text="{value: Message}" />
```

When a user clicks the button, the following things happen:
- The `Foo` method gets called on the server
- The `Message` property changes to _Bar_
- The content of the `TextBox` changes to _Bar_ because it's bound to `Message`

Add four `Button` controls to the `<body>` element. Command-bind them to the `Add`, `Subtract`, `Multiply`, and `Divide` methods we created earlier.