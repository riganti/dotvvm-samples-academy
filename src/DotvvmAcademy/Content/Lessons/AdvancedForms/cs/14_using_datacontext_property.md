Použití vlastnosti DataContext
==============================
Momentálně nejsou bindy na stráce správně, protože viewmodel neobsahuje vlastnosi `FirstName` a další. Přesunuli jsme je do samostatné třídy.        

Aby spojení zase fungovalo, musíme ho napsat ve tvaru `NewCustomer.FirstName` (musíme se odkázat na kontext jiného objektu).

V praxi se chceme těmto komplikovaným zapisům vyhnout a proto se využívá vlastnost `DataContext`.
Tuto vlastnost můžete nastavit na kterémkoli elementu nebo komponentě.

Tato vlastnost řiká, že všechny bindy uvnitř nastaveného elementu jsou vlastnostmi objektu nastavenému ve vlastnosti `DataContext`

Nastavte vlastnost DataContext tagu `p` tímto způsobem : `<p DataContext="{value: NewCustomer}">`. Samotné databindingy se tím pádem nemusí přeposovat.

[<DothtmlExercise Initial="samples/CustomerDetailView_Stage9.dothtml"
        Final="samples/CustomerDetailView_Stage10.dothtml"
        DisplayName="CustomerDetailView.dothtml"
        ValidatorId="Lesson3Step11Validator" />]