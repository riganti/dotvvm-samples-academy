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

## Summary

- Binding context je místo, vůči kterému se vyhodnocují výrazy data-bindingu.
- Každá komponenta má vlastnost `DataContext`.
- Komponenty přebírají `DataContext` z nadřazených komponenta.
- `DataContext` celé stránky je celý ViewModelu.
- Pokud je `DataContext` komponenty nastaven na `null`, tato komponenta se skryje.
