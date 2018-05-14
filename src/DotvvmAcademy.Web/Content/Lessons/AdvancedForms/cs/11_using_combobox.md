Použití komponenty ComboBox (rozbalovací nabídka)
=================================================
Přidjete na stránku komponentu `ComboBox` a napojte vlastnost `DataSource` na kolekci `Countries` ve viewmodelu. Toto naplní rozbalovací nabídku objekty, které jsme inicializovali v konstruktoru.
        
Teď musíme specifikovat která vlastnost objektu, kterými jsme naplnili komponentu ComboBox, se má v nabídce zobrazovat. Nastavte vlastnost komponenty `DisplayMember` na "Name".

Jelikož chceme ukládat jen Id země, kterou z nabídky vybíráme, nastavte vlastnost komponenty `ValueMember` na "Id".

A jelikož chceme vybranou hodnotu ukládat, nastavte `SelectedValue` na vlastnost `SelectedCountryId` ve viewmodelu. 

[<DothtmlExercise Initial="samples/CustomerDetailView_Stage7.dothtml"
        Final="samples/CustomerDetailView_Stage8.dothtml"
        DisplayName="CustomerDetailView.dothtml"
        ValidatorId="Lesson3Step11Validator" />]