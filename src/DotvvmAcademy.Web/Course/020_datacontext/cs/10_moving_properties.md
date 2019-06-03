---
Title: Přesouvání vlastností
Moniker: moving-properties
CodeTask:
    Path: 10_moving_properties.csharp.csx
    Default: ProfileDetailViewModel_10.cs
    Correct: ProfileDetailViewModel_20.cs
---

# Přesouvání vlastností

Vítejte zpět v DotVVM Academy. V této lekci vytvoříme jednoduchou stránku "Můj profil"!

U složitějších stránek se často stává, že ve ViewModelu najdeme vnořené objekty. Aby naše data-bindingy byly krátké a přehledné, můžeme na určité části View změnit tzv. __binding context__.

Binding context (nastavovaný pomocí vlastnosti `DataContext`) je místo ve ViewModelu, k němuž se vztahují výrazy v data-bindingu. 

Chytrou prací s binding contextem lze dosáhnout toho, aby se data-bindingy zkrátily a zpřehlednily, a také pomocí něj můžeme zobrazovat či skrývat určité části View.

Začněme tím, že údaje o uživateli dáme do vlastního objektu, a ten pak umístíme do ViewModelu.

---

## Tasks

- Přesuňte vlastnosti `FirstName` a `LastName` do třídy `Profile`.
- Vytvořte vlastnost typu `Profile` ve ViewModelu a pojmenujte ji `Profile`.
