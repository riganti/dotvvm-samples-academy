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

Skvělá práce! Můžete si vyzkoušet interaktivní ukázku, abyste validaci viděli v akci.

> Poznámka: Jako přihlašovací údaje použijte `john@example.com` a `CorrectHorseBatteryStaple`.

---

## Shrnutí

- `DataAnnotation` atributy jsou nejjednodušší formou validace.
- Komponenta `Validator` může být použita k zobrazení validačních chyb.
- Další validační chyby můžete přidávat uvnitř commandů za použití metody `AddModelError`.

> Poznámka: Existují i další validační mechanismy jako [IValidatableObject](https://www.dotvvm.com/docs/tutorials/basics-validation/latest), komponenta [ValidationSummary](https://www.dotvvm.com/docs/tutorials/basics-validator-controls/latest) nebo vlastnost [Validation.Target](https://www.dotvvm.com/docs/tutorials/basics-validation-target/latest), ty jsou však nad rámec této lekce.