---
Title: Validator
Moniker: validator
CodeTask:
    Path: 20_validator.dothtml.csx
    Default: LogIn_10.dothtml
    Correct: LogIn_20.dothtml
    Dependencies:
        - LogInViewModel_20.cs
        - .solution/LogIn/AccountService.cs
---

# Validator

Uvnitř View lze použít komponentu `Validator`. V případě, že je zadaná vlastnost nevalidní, komponenta zobrazí chybovou hlášku:

```dothtml
<dot:Validator Value="{value: Email}"
               ShowErrorMessageText="true" />
```

---

## Úkoly

- Přidejte vedle každé komponenty `TextBox` komponentu `Validator`.
- Nastavte vlastnost `ShowErrorMessageText` na `true`.
- Nastavte vlastnost `Value` na stejnou vlastnost ViewModelu, na kterou se odkazuje příslušný `TextBox`.
