---
Title: Metoda Add
Moniker: add-method
CodeTask:
    Path: 30_add_method.csharp.csx
    Default: ToDoViewModel_30.cs
    Correct: ToDoViewModel_40.cs

---

# Metoda Add

Chceme míz možnost přidávat a odebírat prvky z kolekce.

V dalším kroce přidáme `TextBox` a `Button`, abychom umožnili uživateli přidat nový prvek seznamu.

```dothtml
<dot:TextBox Text="{value: NewItem}" />
<dot:Button Click="{command: Add()}"
            Text="Add Item" />
```

Nejdříve však musíme udělat něco jiného.

---

## Úkoly

Pojďme napsat logiku přidávání jako první.

- Přidejte propertu typu `string` pojmenovanou `NewItem` do viewmodelu.
- Uvnitř metody `Add` vytvořte nový objekt `ToDoItem` a inicializujte jeho propertu `Text` na `NewItem`. Poté přidejte instanci do `Items`.
