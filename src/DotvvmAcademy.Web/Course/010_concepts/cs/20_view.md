---
Title: View
Moniker: view
CodeTask:
    Path: 20_view.dothtml.csx
    Default: Counter_10.dothtml
    Correct: Counter_20.dothtml
    Dependencies:
        - CounterViewModel_20.cs

---

# View

__Views__ jsou rozhraním, které uživatelé vidí a se kterým interagují. V DotVVM se píší v jazyce __DotHTML__, což je takové HTML na steroidech.

Dvěma nejzřejmějšími změnami oproti čistému HTML jsou:

- Řádek začínající `@viewModel` - tento řádek je tzv. __ViewModel Directive__. Říká DotVVM, který ViewModel k tomuto View patří.
- __Value Bindings__ - výrazy, které provazují konkrétní properties z ViewModelu se specifickými místy ve View:

```dothtml
<p>{{value: Result}}</p>
```

Když uživatel načte stránku s tímto příkladem, hodnota property `Result` se zobrazí uvnitř elementu `<p>`. Jestliže se `Result` později změní, obsah odstavce se automaticky aktualizuje.

---

## Úkoly

- Do elementu `<body>` přidejte element `<p>`.
- Za pomoci __Value Binding__ výrazu zobrazte hodnotu `Result` property z ViewModelu uvnitř nově přidaného `<p>` elementu.
