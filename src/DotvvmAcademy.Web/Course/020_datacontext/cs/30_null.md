---
Title: Null DataContext
Moniker: null-datacontext
CodeTask:
    Path: 30_null_datacontext.csharp.csx
    Default: ProfileDetailViewModel_30.cs
    Correct: ProfileDetailViewModel_40.cs
---

# Null DataContext

Ještě jsme neinicalizovali propertu `Profile` a proto je její hodnota `null`. Právě proto nebude celý `<div>` a jeho obsah vykreslen. Tuto funkci můžeme použít k tomu, abychom skryli určité části stránky.

---

## Úkoly

Pojďme napsat kód abychom inicializovali propertu `Profile` na vyžádání.

- V metodě `Load` inicializujte propertu `Profile` na novou instanci `Profile`.
- V metodě `Unload` nastavte propertu `Profile` na `null`.