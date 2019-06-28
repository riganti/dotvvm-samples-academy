---
Title: Metoda Remove
Moniker: remove-method
CodeTask:
    Path: 50_remove_method.csharp.csx
    Default: ToDoViewModel_50.cs
    Correct: ToDoViewModel_60.cs
---

# Metoda Remove

Nyní bychom rádi umožnili uživateli úkoly ze seznamu odebírat.

Metoda `Remove` potřebuje vědět, který prvek z kolekce má odebrat. Můžeme jí ho předat jako parametr.

```dothtml
<dot:Button Click="{command: _root.Remove(_this)}"
            Text="Remove Item" />
```

---

## Úkoly

- Uvnitř metody `Remove` vymažte parametr `item` z kolekce `Items`.
