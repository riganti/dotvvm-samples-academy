---
Title: Přesouvání vlastností
Moniker: moving-properties
CodeTask:
    Path: 10_moving_properties.csharp.csx
    Default: ProfileDetailViewModel_10.cs
    Correct: ProfileDetailViewModel_20.cs
---

# Přesouvání vlastností

Vítejte zpět v DotVVM Academy. Pojďme udělat 'Edit profile' stránku! Nejdříve potřebujeme pročistit ViewModel.

__Binding Context__ (`DataContext` property) je cíl vazeb, je to objekt jehož členy jsou použity ve výrazu. You can change the Binding Context to shorten your bindings and also hide specific parts of the View.

Often it is convenient to have more complex objects in the ViewModel. Clever use of Binding Context can help to better structure the View.

---

## Tasks

- Přesuňte `FirstName` a `LastName` property do třídy `Profile`.
- Vytvořte property typu `Profile` ve viewmodelu. Pojmenujte ji `Profile`.
