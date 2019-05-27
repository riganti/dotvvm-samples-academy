---
Title: Tlačítko Remove
Moniker: remove-button
CodeTask:
    Path: 60_remove_button.dothtml.csx
    Default: ToDo_30.dothtml
    Correct: ToDo_40.dothtml
---

# Tlačítko Remove

Na konec potřebujeme pro každou položku tlačítko "Remove".

Vzhledem k tomu, že metoda `Remove` nepatří do `ToDoItem`, potřebujeme přistupovat k Binding Contextu `_root`. Budeme také potřebovat předat aktuální prvek jako parameter příkazu. Můžeme použíz `_this`, abychom odkazovali na aktuální položku.

---

## Tasks

- Přidejte `Button` do `Repeater` šablony.
- Toto tlačítko musí volat `Remove` v `_root` s `_this` jako parametr.
