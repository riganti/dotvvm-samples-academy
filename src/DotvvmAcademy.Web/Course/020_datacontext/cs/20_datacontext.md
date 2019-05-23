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

Defaultně je `DataContext` property všech komponent nastaveny na samotný viewmodel. Můžete nastavit jiný `DataContext` jakémukoliv komponentu a ten poté bude platit i pro všechny potomky komponenty.

```dothtml
<div DataContext="{value: Profile}">
    <dot:TextBox Text="{value: FirstName}"/>
</div>
```

---

## Tasks

- Propojte `DataContext` vlastnost elementu `<div>` s vlastností `Profile` z viewmodelu.
- Nastavte binding expression uvnitř elementu `<div>` tak, aby respektovala nový `DataContext`.
