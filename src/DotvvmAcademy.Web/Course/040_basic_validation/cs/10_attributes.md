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

V této lekci budeme validovat login formulář.

__Attributes__ z `System.ComponentModel.DataAnnotations` namespace poskytují nejjednodušší způsob validace uživatelského vstupu v DotVVM.

---

## Úkoly

- Přidejte atribut `[Required]` k propertám `Email` a `Password`.
- Přidejte atribut `[EmailAddress]` k propertě `Email`.