﻿---
Title: Commands
Moniker: commands
CodeTask:
    Path: 50_commands.dothtml.csx
    Default: Counter_30.dothtml
    Correct: Counter_40.dothtml
    Dependencies: 
        - CounterViewModel_40.cs
---

# Commands

The __Command Binding__ can be used to call viewmodel methods from the view.

Commands can be used from many controls. Here you can see how it would be used in the `Button` control:

```dothtml
<dot:Button Click="{command: Add()}"
            Text="Add" />
```

Changes made by the `Add` method will be sent back to the browser and the page will be updated.

---

## Tasks

- Add two `<dot:Button />` controls to the `<body>` element.
- Use Command Bindings to call the `Add` and `Subtract` methods you created earlier by clicking on the buttons.
