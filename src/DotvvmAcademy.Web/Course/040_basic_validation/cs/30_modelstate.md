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

Validace je provedena předtím, než se zavolá jakýkoliv command. Můžete také vytvořit vlastní validační errory uvnitř commandu za použití `AddModelError` metody:

```csharp
this.AddModelError(vm => vm.ValidatedProperty, "An error.");
Context.FailOnInvalidModelState();
```

Lambda výraz identifikuje propertu, která je v neplatném stavu. Volání `FailOnInvalidModelState` přeruší probíhající dotaz, pokud se objeví nějaké errory. Tyto errory se zobrazí uživateli.

---

## Úkoly

- Uvnitř metody `LogIn`:
    - Zavolání `accountService.LogIn(Email, Password)`. Tato metoda navrací `bool`.
    - Pokud předchozí zavolání vrátí `false`, reportujte validační error použitím `this.AddModelError`.
    - Použijte `vm => vm.Email` jako identifikátor validované property.