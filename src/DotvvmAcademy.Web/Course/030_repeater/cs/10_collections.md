---
Title: Kolekce
Moniker: collections
CodeTask:
    Path: 10_collections.csharp.csx
    Default: ToDoViewModel_10.cs
    Correct: ToDoViewModel_20.cs
---

# Kolekce

Binding context se může měnit uvnitř některých komponent, jako třeba `Repeater`. Tuto komponentu si představíme během vytváření jednoduchého seznamu úkolů.

---

## Úkoly

- Přidejte vlastnost typu `List<ToDoItem>` pojmenovanou `Items` do ViewModelu a inicializujte ji na nový prázdný seznam.

> Poznámka: ViewModel může obsahovat jakoukoliv kolekci konkrétního typu jako například `List`, `HashSet`, a `Dictionary`. Nedoporučujeme používat rozhraní kolekcí jako například `IEnumerable`, `IList`, and `IDictionary`.
