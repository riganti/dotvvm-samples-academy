---
Title: Zpět do Root
Moniker: back-to-root
CodeTask:
    Path: 40_back_to_root.dothtml.csx
    Default: ProfileDetail_20.dothtml
    Correct: ProfileDetail_30.dothtml
---

# Zpět do Root

Potřebujeme tlačítka k zavolání `Load` a `Unload` metody.

I v rámci Binding Contextu, který je hluboko v hierarchii, můžete přistoupit k viewmodelu za použití `_root` pseudo-proměnné:

```dothtml
<dot:Button Click="{command: _root.Create()}"
            Text="Create" />
```

> Existuje celá řada [binding pseudo-proměnných](https://www.dotvvm.com/docs/tutorials/basics-binding-context/latest) jako je `_parent`, `_collection` nebo `_this`.

---

## Úkoly

- Přidejte komponentu `<dot:Button>` _mimo_ `<div>` a command-provázání její `Click` property s metodou `Load`.
- Přidejte komponentu `<dot:Button>` , který zavolá metodu `Unload` _uvnitř_ `<div>` elementu.
