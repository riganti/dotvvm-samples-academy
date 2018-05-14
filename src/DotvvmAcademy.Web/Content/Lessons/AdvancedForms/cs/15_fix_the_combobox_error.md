Řešení chyby komponenty ComboBox
================================
Teď máme chybu v kódu, protože vlastnost `DataSource` komponenty `ComboBox` je nastavena na `Countries`. Objekt `NewCustomer` tuto vlastnost nemá,
protože celkový seznam zemí nemá nic společného s informací o klientovi a proto byl přesunut

Tato kolekce se nachází ve viewmodelu, proto musíme bytvořit bind na rodičovský kontext.

Tento problém se dá byřešit jednoduše tak, že se bind vlastnosti `DataSource` komponenty `ComboBox` přepíše na `_parent.Countries`.

> Při zápisu každého spojení (binding expression) se dájí použít hodnoty jako `_parent`, `_this` a `_root`.
> * `_parent` reprezenture kontext rodičovského objektu
> * `_this` aktuální kontext
> * `_root` kontext viewmodelu

[<DothtmlExercise Initial="samples/CustomerDetailView_Stage11.dothtml"
        Final="samples/CustomerDetailView_Stage11.dothtml"
        DisplayName="CustomerDetailView.dothtml"
        ValidatorId="Lesson3Step11Validator" />]