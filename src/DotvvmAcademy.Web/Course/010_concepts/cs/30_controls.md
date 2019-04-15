---
Title: Komponenty
Moniker: controls
CodeTask:
    Path: 30_controls.dothtml.csx
    Default: Counter_20.dothtml
    Correct: Counter_30.dothtml
---

# Komponenty

Komponenty (controls) jsou znovupoužitelné části views. Dají se poznat podle prefixu `<dot:`.

```dothtml
<dot:TextBox Text="{value: Difference}" />
```

`TextBox` je ve své podstatě element `<input type="text">`, který dovede pracovat s binding výrazy.

> Poznámka: Technicky je každý standardní HTML element zároveň komponentou v DotVVM, konkrétně komponentou _HtmlGenericControl_.

---

## Úkoly

- Přidejte do `<body>` komponentu `<dot:TextBox>`.
- Pomocí value binding výrazu provažte property `Difference` s atributem `Text` právě přidané komponenty `TextBox`.
