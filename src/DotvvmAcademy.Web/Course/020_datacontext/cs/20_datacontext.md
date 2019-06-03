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

Ve výchozím nastavení obsahuje vlastnost `DataContext` každé komponenty referenci na celý ViewModel. Kterémukoliv elementu nebo komponentě můžete ale `DataContext` změnit, čímž docílíte změny binding contextu. 

Jak je vidět na ukázkovém příkladu, data-binding se odkazuje na vlastnost `FirstName`, která je definována v objektu `Profile` a ne přímo ve ViewModelu.

```dothtml
<div DataContext="{value: Profile}">
    <dot:TextBox Text="{value: FirstName}"/>
</div>
```

---

## Tasks

- Propojte vlastnost `DataContext` elementu `<div>` s vlastností `Profile` z ViewModelu.
- Upravte bindingy uvnitř elementu `<div>` tak, aby respektovaly nový binding context.
