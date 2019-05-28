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

- __View__ definuje vzhled stránky. Strukturou je založen na formátu HTML, ale obsahuje pár věcí navíc.
- __ViewModel__ ukládá stav stránky (obsahuje např. data z formulářů) a zpracovává interakce s uživatelem (definuje např. co se stane při kliknutí na tlačítko).

Stav stránky ve ViewModelu reprezentujeme pomocí public vlastností.

```csharp
public int Result { get; set; }
```

---

## Úkoly

Začneme s ViewModelem.

- Přidejte do ViewModelu dvě `public` vlastnosti typu `int` s názvy `Result` a `Difference`.
