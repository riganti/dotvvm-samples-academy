---
Title: Metody
Moniker: methods
CodeTask:
    Path: 40_methods.csharp.csx
    Default: CounterViewModel_30.cs
    Correct: CounterViewModel_40.cs
---

# Metody

Metody ViewModelu se dají volat z View. Pomocí nich budeme implementovat funkcionalitu přičítání a odčítání hodnoty v našem počítadle.

```csharp
public void Add() 
{
    Result = ...
}
```

> Poznámka: DotVVM posílá pří volání metody AJAX POST požadavek, při kterém se požadovaná metoda spustí na serveru a změny se odešlou zpět uživateli.

---

## Úkoly

- Přidejte metodu `Add`, která přičte hodnotu `Difference` k `Result` a výsledek uloží do `Result`.
- Přidejte metodu `Subtract`, která analogicky hodnotu `Difference` od `Result` odečte.
