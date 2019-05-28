---
Title: Null DataContext
Moniker: null-datacontext
CodeTask:
    Path: 30_null_datacontext.csharp.csx
    Default: ProfileDetailViewModel_30.cs
    Correct: ProfileDetailViewModel_40.cs
---

# Null DataContext

Vlastnost `Profile` jsme ještě neinicializovali, a proto je její hodnota `null`. 

Z tohoto důvodu DotVVM celý `<div>` skryje a nebude vyhodnocovat výrazy uvnitř. Tento trik můžeme použít k tomu, abychom určité části stránky skryli.

---

## Úkoly

Pojďme napsat kód, který zinicializuje vlastnost `Profile`.

- V metodě `Load` do vlastnosti `Profile` přiřaďte novou instanci třídy `Profile`.
- V metodě `Unload` nastavte vlastnost `Profile` na `null`.