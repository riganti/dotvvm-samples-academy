---
Title: Commands
CodeTask: /resources/010_calculator/50_commands.dothtml.csx
---

# Commands

Commands are just a different kind of binding. They invoke a function on the server. Let's take a look at a `Button` sample:

^^^
```csharp
public string Message { get; set; }

public void Foo()
{
    Message = "Bar";
}
```
^^^ A ViewModel Excerpt


^^^
```dothtml
<dot:Button Click="{command: Foo()}" />
<dot:TextBox Text="{value: Message}" />
```
^^^ A View Excerpt

When a user clicks the button, the following things happen:
- The `Foo` method gets called on the server
- The `Message` property changes to _Bar_
- The content of the `TextBox` changes to _Bar_ because it's bound to `Message`

Add four `Button` controls to the `<body>` element. Command-bind them to the `Add`, `Subtract`, `Multiply`, and `Divide` methods we created earlier.