---
Title: Attributes
Moniker: attributes
CodeTask:
    Path: 10_attributes.csharp.csx
    Default: LogInViewModel_10.cs
    Correct: LogInViewModel_20.cs
    Dependencies: 
        - .solution/LogIn/AccountService.cs
---

# Attributes

V této lekci se naučíme používat validaci na jednoduchém příkladu přihlašovací stránky.

Validace v DotVVM využívá standardní atributy z namespace `System.ComponentModel.DataAnnotations`.

---

## Úkoly

- Přidejte atribut `[Required]` nad vlastnosti `Email` a `Password`.
- Přidejte atribut `[EmailAddress]` nad vlastnost `Email`.