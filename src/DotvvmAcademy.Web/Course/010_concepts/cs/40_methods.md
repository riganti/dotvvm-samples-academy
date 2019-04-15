---
Title: Metody
Moniker: methods
CodeTask:
    Path: 40_methods.csharp.csx
    Default: CounterViewModel_30.cs
    Correct: CounterViewModel_40.cs
---

# Metody

Metody ViewModelu se dají volat z View. Implemetujeme pomocí nich funkcionalitu přičítání a odčítání hodnoty v počítadle.

> Poznámka: DotVVM posílá pří volání metody AJAXový požadavek, při kterém se požadovaná metoda spustí na serveru a změny se odešlou zpět uživateli.

---

## Úkoly

- Napište metodu `Add` takovou, že přičte hodnotu `Difference` k `Result` a výsledek uloží do `Result`.
- Napište metodu `Subtract`, která analogicky hodnotu `Difference` od `Result` odečte.
