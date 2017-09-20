Přidání nového úkolů
====================
Teď je na čase naimplementovat metodu `AddTask()`. Tato metoda by měla přidávat nový objekt typu `TaskData`
s vlastností `Title` nastavenou na `AddedTaskTitle`.

Po přidání objektu také potřebujeme nastavit vlastnost `AddedTaskTitle` na prázdný řetězec.

[<CSharpExercise Initial="samples/ToDoListViewModel_Stage5.cs"
                 Final="samples/ToDoListViewModel_Stage6.cs"
                 DisplayName="ToDoListViewModel.cs"
                 ValidatorId="Lesson2Step7Validator">
    <Dependencies>
        <Dependency>samples/TaskData_Stage2.cs</Dependency>
    </Dependencies>
</CSharpExercise>]