---
Title: Kolekce
Moniker: collections
CodeTask:
    Path: 10_collections.csharp.csx
    Default: ToDoViewModel_10.cs
    Correct: ToDoViewModel_20.cs
---

# Kolekce

Binding Context se může implicitně měnit v kontrolkách jako je `Repeater`. To si ukážeme vytvořením seznamu úkolů.

---

## Úkoly

- Přidejte propertu typu `List<ToDoItem>` pojmenovanou `Items` do viewmodelu a inicializujte ji.

> Poznámka: Viewmodel může obsahovat jakoukoliv kolekci konkrétního typu jako například `List`, `HashSet`, a `Dictionary`. Nedoporučujeme používat rozhraní kolekcí jako například `IEnumerable`, `IList`, and `IDictionary`.
