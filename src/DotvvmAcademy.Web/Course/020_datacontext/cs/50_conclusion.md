---
Title: Závěr
Moniker: conclusion
EmbeddedView:
    Path: .solution/ProfileDetail/Views/ProfileDetail.dothtml
    Dependencies:
        - .solution/ProfileDetail/ViewModels/ProfileDetailViewModel.cs
        - .solution/ProfileDetail/ViewModels/Profile.cs
Archive: 
    Path: .solution
---

# Závěr

Dobrá práce! Nyní již víte co je `DataContext`!

---

## Summary

- Binding Context je výchozím cílem provázání.
- Každá kontrolka má uvnitř sebe svou vlastní `DataContext` propertu.
- Defaultně kontrolky získávají jejich `DataContext` od jejich rodičovských kontrolek.
- `DataContext` v kořenu dokumentu je jeho viewmodel.
- Pokud je `DataContext` kontrolky nastaven na `null`, kontrolka se nevykreslí.
