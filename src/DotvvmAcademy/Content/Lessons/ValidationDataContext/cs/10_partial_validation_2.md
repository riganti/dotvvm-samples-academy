Èásteèná validace II
====================
Úplnì vypnout validaci mùžete pøidáním `Validation.Enabled="false"`. Toto nastavení se taktéž aplikuje na všechny potomky elementu, 
na kterém je tato vlastnost nastavená.
        
Všimìte si, že se validace spouští bindingem: `{command: nejakyPrikaz()}`.

Proto se vlastnost `Validation.Enabled` nebo `Validation.Target` musí nastavovat na tlaèítku nebo komponentì, která má binding na pøíkaz.
Vypnutí validace na jednotlivých textových polích formuláøe nebude mít žádný efekt.