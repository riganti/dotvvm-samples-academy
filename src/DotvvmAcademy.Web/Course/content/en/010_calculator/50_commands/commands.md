---
Title: Commands
CodeTask: /resources/010_calculator/50_commands.dothtml.csx
---

# Commands

Commands are another kind of binding. They invoke a function on the server. Let's take a look at a `Button` sample:

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

When a user clicks the button, the `Foo` method gets called on the server and the content of the `TextBox` changes to _Bar_ because the `Message` property changed.

Add four `Button` controls to the `<body>` element. Command-bind them to the `Add`, `Subtract`, `Multiply`, and `Divide` methods respectively.