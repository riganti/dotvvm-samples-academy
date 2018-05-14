Použití Validation.InvalidCssClass
==================================
Pomocí vlastnosti `Validator.Value` jsme oznaèili elementy, které se budou mìnit v závislosti 
na validaèních nastaveních jednotlivých vlastností.
        
Teï musíme specifikovat, co se má stát, když vlastnost nebude validní.
Dejme tomu, že bychom chtìli pøidat CSS tøídu `has-error` každému tagu `div`, když nebude validní.
CSS tøída `has-error` zvýrazní textové pole uvnitø tohoto tagu.

Pøidejte vlastnost `Validator.InvalidCssClass="has-error"` každému tagu `div`.

[<DothtmlExercise Initial="../samples/CustomerDetailView_Stage3.dothtml"
         Final="../samples/CustomerDetailView_Stage4.dothtml"
         DisplayName="CustomerDetailView.dothtml"
          ValidatorId="Lesson4Step4Validator" />]