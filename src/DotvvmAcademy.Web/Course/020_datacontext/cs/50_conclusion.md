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

Dobrá práce! Nyní již víte, jak funguje `DataContext`.

---

## Shrnutí

- Binding context je objekt, vůči kterému se vyhodnocují výrazy data-bindingu.
- Každá komponenta má vlastnost `DataContext`.
- Komponenty přebírají `DataContext` z nadřazených komponent.
- Kořenový `DataContext` je celý ViewModel.
- Pokud je `DataContext` komponenty nastaven na `null`, tato komponenta se skryje.
