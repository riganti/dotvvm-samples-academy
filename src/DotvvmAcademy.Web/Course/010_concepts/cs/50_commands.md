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

__Command bindings__ umožňují volání metod viewmodelu z view.

Commands se dají použít z mnoha komponent, např. z tlačítkové komponenty `Button`:

```dothtml
<dot:Button Click="{command: Add()}"
            Text="Add" />
```

Změny, které volání metody `Add` způsobí se odešlou zpět prohlížeči a aplikují se na příslušná místa na stránce.

---

## Úkoly

- Přidejte do `<body>` dvě komponenty `<dot:Button />`.
- Použijte command binding při volání metod `Add` a `Subtract`, když uživatel klikne na příslušné tlačítko.
