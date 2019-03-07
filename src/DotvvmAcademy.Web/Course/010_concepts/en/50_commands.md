---
Title: Commands
CodeTask:
    Path: 50_commands.dothtml.csx
    Default: Counter_30.dothtml
    Correct: Counter_40.dothtml
    Dependencies: 
        - CounterViewModel_40.cs
---

# Commands

The __Command Binding__ can be used to call the ViewModel methods from the View.

Unlike value bindings, command bindings make a request to the server and perform the necessary updates afterwards. Commands can be used from many controls. Here you can see how it would be used in the `Button` control:

```dothtml
<dot:Button Click="{command: Add()}"
            Text="Add" />
```

Changes made by the `Add` method will be sent back to the browser and the page will be updated.

---

## Tasks

- Add two `<dot:Button />` controls to the `<body>` element.
- Use command binding to call the `Add` and `Subtract` methods you created earlier with the added buttons.
