---
Title: Repeater
Moniker: repeater
CodeTask:
    Path: 20_repeater.dothtml.csx
    Default: ToDo_10.dothtml
    Correct: ToDo_20.dothtml
    Dependencies:
        - ToDoViewModel_20.cs
---

# Repeater

Komponenta `Repeater` se používá k vykreslení kolekce položek (například náš seznam úkolů). Kolekce se nabinduje na vlastnost `DataSource`. To, jak budou jednotlivé položky vypadat, určí šablona, kterou je možné uvést uvnitř komponenty:

```dothtml
<dot:Repeater DataSource="{value: Items}">
    <p>{{value: Text}}</p>
</dot:Repeater>
```

Pro každý prvek kolekce `Items` bude vykreslen element `<p>`. Binding context se navíc mění na daný prvek kolekce, můžeme se tedy odkazovat na vlastnost `Text` deklarovanou ve třídě `ToDoItem`.

---

## Úkoly

Pojďme vykreslit kolekci úkolů:

- Dovnitř elementu `<body>` přidejte `Repeater` a jeho vlastnost `DataSource` nabindujte na kolekci `Items`.
- Vykreslete element `<p>` pro každý prvek v kolekci `Items`.
- Uvnitř odstavce vypište vlastnost `Text`.