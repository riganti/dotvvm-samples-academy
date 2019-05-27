---
Title: DataContext
Moniker: datacontext
CodeTask:
    Path: 20_datacontext.dothtml.csx
    Default: ProfileDetail_10.dothtml
    Correct: ProfileDetail_20.dothtml
    Dependencies:
        - ProfileDetailViewModel_20.cs
---

# DataContext

Defaultně jsou `DataContext` property všech komponent nastaveny na samotný viewmodel. Můžete nastavit jiný `DataContext` jakémukoliv komponentu, který bude platit i pro všechny potomky komponentu.

```dothtml
<div DataContext="{value: Profile}">
    <dot:TextBox Text="{value: FirstName}"/>
</div>
```

---

## Tasks

- Propojte `DataContext` propertu elementu `<div>` s propertou `Profile` z viewmodelu.
- Nastavte binding expression uvnitř elementu `<div>` tak, aby respektovala nový `DataContext`.
