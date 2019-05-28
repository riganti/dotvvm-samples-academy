---
Title: ModelState
Moniker: modelstate
CodeTask:
    Path: 30_modelstate.csharp.csx
    Default: LogInViewModel_20.cs
    Correct: LogInViewModel_30.cs
    Dependencies: 
        - .solution/LogIn/AccountService.cs
---

# ModelState

DotVVM spouští validaci před tím, než se zavolá jakýkoliv command. 

Ne všechny validace je možné jednoduše udělat pomocí validačních atributů. Pomocí metody `AddModelError` můžete uvnitř commandu přidávat vlastní validační chyby, které budou pak zobrazeny uživateli stejným způsobem:

```csharp
this.AddModelError(vm => vm.ValidatedProperty, "An error.");
Context.FailOnInvalidModelState();
```

Lambda výraz identifikuje vlastnost, ke které chybová hláška patří. Volání `FailOnInvalidModelState` přeruší vykonávání commandu, pokud je ve stránce jakákoliv validační chyba. Všechny chyby se v tomto případě zobrazí uživateli.

---

## Úkoly

Uvnitř metody `LogIn` proveďte následující:

- Zavolejte `accountService.LogIn(Email, Password)`. Tato metoda zkontroluje, zdali je e-mail a heslo správné, a vrací `true` nebo `false`.
- Pokud předchozí volání vrátí `false`, přidejte validační chybu pomocí `this.AddModelError`.
- Jako identifikátor validované vlastnosti použijte `vm => vm.Email`.