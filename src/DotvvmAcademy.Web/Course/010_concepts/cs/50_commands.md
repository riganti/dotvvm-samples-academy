---
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

__Command bindingy__ umožňují volání metod ViewModelu z View.

Commandy se dají použít z mnoha komponent, např. z komponenty `Button` reprezentující tlačítko:

```dothtml
<dot:Button Click="{command: Add()}"
            Text="Add" />
```

Změny, které volání metody `Add` způsobí, se odešlou zpět do prohlížeče a příslušná místa na stránce se aktualizují.

---

## Úkoly

- Přidejte do `<body>` dvě komponenty `<dot:Button />`.
- Použijte command binding při volání metod `Add` a `Subtract`, když uživatel klikne na příslušné tlačítko.
