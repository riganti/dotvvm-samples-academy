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

Uvnitř view můžeme použít kontrolku `Validator` k tomu aby ukázala validační zprávy z atributů:

```dothtml
<dot:Validator Value="{value: Email}"
               ShowErrorMessageText="true" />
```

---

## Úkoly

- Přidejte kontrolku `Validator` vedle každého `TextBox`.
- Nastavte `ShowErrorMessageText` na `true`.
- Propojte jejich propertu `Value` ke stejným propertám jako mají jejich přilehlé `TextBox` a jejich property `Text`.
