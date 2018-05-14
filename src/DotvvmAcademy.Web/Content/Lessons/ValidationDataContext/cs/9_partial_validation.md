Èásteèná validace
=================
Implicitnì validaci spouští kterékoli tlaèítko a to na celém viewmodelu
Pokud se nìkde ve viewmodelu vyskytne chyba, pøíkaz se neprovede.

Nicménì, viewmodel èasto obsahuje objekty, které nechceme validovat vùbec.

V tomto pøípadì mùžete využít `Validation.Target="{value: NejakaVlastnost}"`. 
Toto se dá aplikovat na kterýkoli element èi komponentu v DotVVM.

Uvnitø tohoto elementu èi komponenty se validace provede pouze nad objektem, který byl dosazen do vlastnosti `Validation.Target`.