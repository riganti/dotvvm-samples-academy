---
Title: Zpět do Root
Moniker: back-to-root
CodeTask:
    Path: 40_back_to_root.dothtml.csx
    Default: ProfileDetail_20.dothtml
    Correct: ProfileDetail_30.dothtml
---

# Zpět do Root

Do stránky potřebujeme přidat tlačítka, která zavolají metodu `Load` a `Unload`.

I v rámci binding contextu, který je zanořen hluboko v hierarchii ViewModelu, můžete přistoupit k rodičovskému ViewModelu za použití speciální proměnné `_root`:

```dothtml
<dot:Button Click="{command: _root.Unload()}"
            Text="Unload" />
```

> Existuje celá řada [speciálních proměnných](https://www.dotvvm.com/docs/tutorials/basics-binding-context/latest) jako jsou `_parent`, `_collection` nebo `_this`.

---

## Úkoly

- Přidejte komponentu `<dot:Button>` _mimo_ element `<div>` a do její vlastnosti `Click` nabindujte metodu `Load`.
- Přidejte komponentu `<dot:Button>` _dovnitř_ elementu `<div>`, které při kliknutí zavolá metodu `Unload`. Nezapomeňte použít `_root` - metoda `Unload` není definována v objektu `Profile`, ale ve ViewModelu.
