Základy validace
================
Vyvolat validaci na stránce mùžete tak, že využijete vlastnosti `Validator.Value`.
V závislosti na nastavení validace se mùže napøíklad pøidat CSS tøída, pokud vlastnost nebude validní.

Vlastnost `Validator.Value` mùžete pøidat na kterýkoli HTML tag nebo DotVVM komponentu.
Pøidejte tuto vlastnost každému tagu `div` a vytvoøte `data-bind` na stejnou vlastnost, na kterou je napojená pøíslušná komponenta `TextBox`.

[<DothtmlExercise Initial="../samples/CustomerDetailView_Stage1.dothtml"
         Final="../samples/CustomerDetailView_Stage2.dothtml"
         DisplayName="CustomerDetailView.dothtml"
          ValidatorId="Lesson4Step3Validator" />]