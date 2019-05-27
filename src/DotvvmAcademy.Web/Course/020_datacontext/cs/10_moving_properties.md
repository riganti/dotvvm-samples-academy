---
Title: Přesouvání vlastností
Moniker: moving-properties
CodeTask:
    Path: 10_moving_properties.csharp.csx
    Default: ProfileDetailViewModel_10.cs
    Correct: ProfileDetailViewModel_20.cs
---

# Přesouvání vlastností

Vítejte zpět v DotVVM Academy. Pojďme udělat 'Edit profile' stránku! Nejdříve potřebujeme pročistit viewmodel.

__Binding Context__ (`DataContext` properta) je cíl vazeb, je to objekt, jehož členy jsou použity ve výrazu. Můžete změnit Binding Context k tomu, aby se zkrátily bindingy a také změnit určité části view.

Občas je vhodné mít komplexnější objekty uvnitř viewmodelu. Chytré použití binding contextu může pomoci lépe strukturovat view.

---

## Tasks

- Přesuňte `FirstName` a `LastName` property do třídy `Profile`.
- Vytvořte propertu typu `Profile` ve viewmodelu. Pojmenujte ji `Profile`.
