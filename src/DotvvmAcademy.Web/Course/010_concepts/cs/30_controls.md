---
Title: Komponenty
Moniker: controls
CodeTask:
    Path: 30_controls.dothtml.csx
    Default: Counter_20.dothtml
    Correct: Counter_30.dothtml
---

# Komponenty

Dalším z rozšířním jazyka HTML v DotVVM jsou komponenty (controls). Dají se poznat podle prefixu, např. `<dot:`.

```dothtml
<dot:TextBox Text="{value: Difference}" />
```

`TextBox` je ve své podstatě element `<input type="text">`, který dovede pracovat s data-binding výrazy.

---

## Úkoly

- Přidejte do `<body>` komponentu `<dot:TextBox>`.
- Vytvořte data-binding její vlastnosti `Text` na vlastnosti `Difference` ve ViewModelu.
