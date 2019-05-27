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

Kontrolka `Repeater` vykresluje kolekci prvků z její `DataSource` property za použití šablony:

```dothtml
<dot:Repeater DataSource="{value: Items}">
    <p>{{value: Text}}</p>
</dot:Repeater>
```

Element `<p>` je vykreslen pro každý prvek uvnitř kolekce `Items`. Uvnitř `Repeater`, se Binding Context změní na objekt prvku.

---

## Úkoly

Pojďme zobrazit prvky seznamu.

- Uvnitř elementu `<body>` přidejte `Repeater`, jehož `DataSource` je kolekce `Items`.
- Zobrazte element `<p>` pro každý prvek v kolekci `Items`
- Vložte propertu `Text` do elementu `<p>`.