Zobrazení úkolů
===============
Teď se poďte vrátit zpátky do našeho viewmodelu. Musíme přidat seznam objektů `TaskData`, 
abychom jsme je mohli zobrazit na stránce.

Přidejte vlastnost `Tasks` do viewmodelu. Vlastnost musí být typu `List<TaskData>`
a musí být rovnou inicializováná jako `new List<TaskData>()`.

[<CSharpExercise Initial="samples/ToDoListViewModel_Stage3.cs"
                 Final="samples/ToDoListViewModel_Stage4.cs"
                 DisplayName="ToDoListViewModel.cs"
                 ValidatorId="Lesson2Step6Validator">
    <Dependencies>
        <Dependency>samples/TaskData_Stage2.cs</Dependency>
    </Dependencies>
</CSharpExercise>]