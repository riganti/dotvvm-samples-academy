---
Title: Závěr
Moniker: conclusion
EmbeddedView:
    Path: .solution/LogIn/Views/LogIn.dothtml
    Dependencies:
        - .solution/LogIn/ViewModels/LogInViewModel.cs
        - .solution/LogIn/AccountService.cs
Archive:
    Path: .solution
---

# Závěr

Dobrá práce! Vyzkoušejte interaktivní ukátku, abyste viděli validaci v akci.

> Poznámka: Jediné validní údaje jsou "john@example.com" a "CorrectHorseBatteryStaple".

---

## Schrnutí

- `DataAnnotation` atributy jsou nejjednodušší formou validace.
- Kontrolka `Validator` může být použita k ukazování validačních zpráv.
- Můžete vytvořit vlastní errory uvnitř commandů za použití `AddModelError` metody.

> Poznámka: Esitují i jiné validační koncepty jako [IValidatableObject](https://www.dotvvm.com/docs/tutorials/basics-validation/latest), [ValidationSummary](https://www.dotvvm.com/docs/tutorials/basics-validator-controls/latest) kontrolka, a [Validation.Target](https://www.dotvvm.com/docs/tutorials/basics-validation-target/latest) připojená properta. Ty jsou nad rámec této lekce.