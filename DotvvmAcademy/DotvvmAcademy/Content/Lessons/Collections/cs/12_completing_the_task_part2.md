Splnění úkolu po zmáčknutí tlačítka
===================================
Teď musíme nastavit vlastnost `Click` komponenty `LinkButton` na metodu `CompleteTask`.
        
Všimněte si, že všechny bindingy uvniř komponenty `Repeater` neodkazují na vlastnosti a příkazy ve
viewmodelu, ale na vlastnosti objektu `TaskData`.
Metoda `CompleteTask` je však deklarována o vrstvu výš, ve viewmodelu.

V tomto případě využijeme `_parent.CompleteTask()` a tím se odkážeme na objekt o úroveň výš.
Při dodání argumentů do metody, jsme pořád v kontextu objektu `TaskData`. Můžete využít `_this` a tím
do argumentu dodáme celý objekt.

[<DothtmlExercise Initial="samples/ToDoListView_Stage8.dothtml"
                  Final="samples/ToDoListView_Stage9.dothtml"
                  DisplayName="ToDoListView.dothtml"
                  ValidatorId="Lesson2Step12Validator" />]