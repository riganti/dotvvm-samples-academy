---
Title: Metoda Add
Moniker: add-method
CodeTask:
    Path: 30_add_method.csharp.csx
    Default: ToDoViewModel_30.cs
    Correct: ToDoViewModel_40.cs

---

# Metoda Add

V dalším kroku budeme přidávat komponenty `TextBox` a `Button`, abychom uživateli umožnili přidat nový úkol.

```dothtml
<dot:TextBox Text="{value: NewItem}" />
<dot:Button Click="{command: Add()}"
            Text="Add Item" />
```

Nejdříve však musíme nachystat metodu `Add`, která do kolekce `Items` nový úkol přidá.

---

## Úkoly

- Přidejte do ViewModelu vlastnost typu `string` pojmenovanou `NewItem`.
- Uvnitř metody `Add` vytvořte nový objekt `ToDoItem` a nastavte jeho vlastnost `Text` na hodnotu z vlastnosti `NewItem`. 
- Poté tento objekt přidejte do kolekce `Items`.
