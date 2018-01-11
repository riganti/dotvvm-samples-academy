Schování tlačítek
=================
Komponenta `LinkButton` by měla být zobrazená pouze v případě, že úkol ještě není splněn.        
        
V DotVVM má kazdý HTML tag nebo komponenta vlastnost `Visible`.
Nastavením této vlastnosti můžeme schovávat nebo zobrazovat jednotlivé komponenty a tagy.

Propojete vlastnost `IsCompleted` každého úkolu s vlastností `Visible` komponenty `LinkButton`.
Tím docílíme toho, že se tlačítka zobrazí pouze u ukolů, co ještě nejsou hotové.

[<DothtmlExercise Initial="../samples/ToDoListView_Stage7.dothtml"
                  Final="../samples/ToDoListView_Stage8.dothtml"
                  DisplayName="ToDoListView.dothtml"
                  ValidatorId="Lesson2Step10Validator" />]

> Vlastnost `Visible` může být v DotVVM nastavena na kterémkoli elementu v HTML