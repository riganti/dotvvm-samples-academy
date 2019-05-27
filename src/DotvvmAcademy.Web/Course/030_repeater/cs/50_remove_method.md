---
Title: Metoda Remove
Moniker: remove-method
CodeTask:
    Path: 50_remove_method.csharp.csx
    Default: ToDoViewModel_50.cs
    Correct: ToDoViewModel_60.cs
---

# Metoda Remove

Umožníme uživateli odebrat jakýkoliv prvek z kolekce.

Metoda `Remove` potřebuje vědět který prvek má odebrat z kolekce. Můžeme ho předat jako parametr funkce.

```dothtml
<dot:Button Click="{command: _root.Remove(_this)}"
            Text="Remove Item" />
```

---

## Úkoly

- Uvnitř metody `Remove` vymažte parametr `item` z `Items`.
