Globální validaèní nastavení
============================
Uvádìt vlastnost `Validator.InvalidCssClass` na každém validovaném elementu je otravné.       
Nicménì tuto vlastnost mùžete použít na kterémkoli elementu a to se promítne na všechny potomky.

Takže tuto vlastnost mùžete pøidat globálnì tøeba na element `body`.
        
Teï mùžeme smazat vlastnost `Validator.InvalidCssClass` ze všech tagu `div` a obalit je elementem `form`, kterému tuto vlastnost pøidáme.     

[<DothtmlExercise Initial="../samples/CustomerDetailView_Stage5.dothtml"
         Final="../samples/CustomerDetailView_Stage6.dothtml"
         DisplayName="CustomerDetailView.dothtml"
          ValidatorId="Lesson4Step5Validator" />]