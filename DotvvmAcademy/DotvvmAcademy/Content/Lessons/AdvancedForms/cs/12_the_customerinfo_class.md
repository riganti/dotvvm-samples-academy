Třída CustomerInfo
======================
V předchozích krocích jsme přidali všechny vlastnosti  `FirstName`, `LastName`, `Role` a `SelectedCountryId` do stejné třídy jako kolekce `Countries`.

Běžně se to nedělá, protože se v jedné třídě míchají údaje konkretního uživatele a další data, které s uživatelem nemají nic společného.
V našem případě je to např. kolekce `Countries`.

Je dobrý nápad přenést všechny vlastnosti, co v sobě uchovavají informace o konkretním uživateli do samostatné třídy. Třídy tohoto typu obvykle nemají žádné metody, ale jen vlastnosti.

Tyto třídy jsou nazvané DTO - Data Transfer Object (objekt přenosu dat)

Takže, jsme přenesli potřebné vlastnosti do samostatné třídy `CustomerInfo`:

```CSHARP
public class CustomerInfo
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public int SelectedCountryId { get; set; }
}
```

The `Countries` collection stays in the viewmodel.