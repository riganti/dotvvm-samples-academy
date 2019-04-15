---
Title: Začínáme
Moniker: getting-started
CodeTask:
    Path: 10_viewmodel.csharp.csx
    Default: CounterViewModel_10.cs
    Correct: CounterViewModel_20.cs
---

# Začínáme

Vítejte v DotVVM Academy! V této lekci napíšete jednoduché počítadlo, které umožní uživatelům opakovaně přičítat a odčítat čísla od aktuální hodnoty.

> Používání DotVVM vyžaduje základní znalost jazyků [HTML](https://www.sololearn.com/Course/HTML/) a [C#](https://www.sololearn.com/Course/CSharp/). Pokud s nimi nemáte zkušenosti, věnujte jim trochu času, abyste se v našich lekcích neztratili.

Jak DotVVM funguje?

Každá stránka v DotVVM se skládá ze dvou souborů:

- __View__ (někdy česky Pohled) obsahuje vzhled stránky. Strukturou je založen na obyčejných HTML dokumentech.
- __ViewModel__ je stavem stránky (obsahuje např. data z formulářů) a zpracovatelem interakce s uživatelem (definuje např. co se stane při kliknutí na tlačítko).

Stav stránky je obsažen v C# properties.

```csharp
public int Result { get; set; }
```

---

## Úkoly

Začneme s viewmodelem.

- Přidejte do viewmodelu dvě `public` property typu `int` jmenující se `Result` a `Difference`.
