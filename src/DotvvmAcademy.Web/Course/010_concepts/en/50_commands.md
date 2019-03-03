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

The __Commands binding__ allows to call the ViewModel methods from the View.

Unlike the value bindings, the command bindings make a request to the server and perform the necessary updates afterwards. The commands can be used from many controls, the `Button` for example:

```dothtml
<dot:Button Click="{command: Add()}"
            Text="Add" />
```

The changes made by the `Add` method will be sent back to the browser and the page will be updated.

---

## Tasks

- Add two `<dot:Button>` controls to the `<body>` element.
- Use command binding to call the `Add` and `Subtract` methods you created earlier.