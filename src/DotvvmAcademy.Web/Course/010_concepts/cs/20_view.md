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

__Views__ jsou rozhraním, které uživatelé vidí a se kterým interagují. V DotVVM se píší v jazyce __DotHTML__, což je HTML s několika přidanými syntaktickými konstrukcemi.

Dvěma nejběžnějšími změnami oproti čistému HTML jsou:

- Řádek začínající `@viewModel` - tento řádek je tzv. __direktiva__. Říká DotVVM, který ViewModel patří k tomuto View.
- __Value bindingy__ - výrazy, pomocí nichž provádíme tzv. data-binding z vlastností ViewModelu na konkrétní místa ve View:

```dothtml
<p>{{value: Result}}</p>
```

Když uživatel načte stránku s tímto příkladem, hodnota property `Result` se zobrazí uvnitř elementu `<p>`. Pokud se `Result` později změní, obsah odstavce se automaticky aktualizuje.

---

## Úkoly

- Do elementu `<body>` přidejte element `<p>`.
- Za pomoci __value bindingu__ zobrazte hodnotu vlastnosti `Result` z viewmodelu uvnitř nově přidaného `<p>` elementu.
