---
Title: Zpět ke kořenům
Moniker: back-to-root
CodeTask:
    Path: 40_back_to_root.dothtml.csx
    Default: ProfileDetail_20.dothtml
    Correct: ProfileDetail_30.dothtml
---

# Zpět ke kořenům

Do stránky potřebujeme přidat tlačítka, která zavolají metodu `Load` a `Unload`.

I v rámci binding contextu, který je zanořen hluboko v hierarchii ViewModelu, můžete přistoupit ke kořenu (celému ViewModelu) za použití speciální proměnné `_root`:

```dothtml
<dot:Button Click="{command: _root.Unload()}"
            Text="Unload" />
```

> Existuje celá řada dalších [speciálních proměnných](https://www.dotvvm.com/docs/tutorials/basics-binding-context/latest) jako jsou `_parent`, `_collection` nebo `_this`.

---

## Úkoly

- Přidejte komponentu `<dot:Button>` _mimo_ element `<div>` a do její vlastnosti `Click` nabindujte metodu `Create`.
- Přidejte komponentu `<dot:Button>` _dovnitř_ elementu `<div>`, které při kliknutí zavolá metodu `Delete`. Nezapomeňte použít `_root` - metoda `Delete` není definována v objektu `Profile`, ale ve ViewModelu.
