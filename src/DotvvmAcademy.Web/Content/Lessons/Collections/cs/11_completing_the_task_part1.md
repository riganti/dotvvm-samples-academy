Splnění úkolu po zmáčknutí tlačítka<
==========================
Když uživatel klikne na komponentu `LinkButton`, musíme spustit metodu ve viewmodelu.        
        
Tato metoda musí vědět, na kterém úkolu byla spuštěná. Můžeme tento úkol poslat jako parametr metody.

Nadeklarujte metodu `CompleteTask` s parametrem typu `TaskData`.
V těle metody nastavte úkolu vlastnost `IsCompleted` na `true`.

[<CSharpExercise Initial="samples/ToDoListViewModel_Stage6.cs"
                 Final="samples/ToDoListViewModel_Stage7.cs"
                 DisplayName="ToDoListViewModel.cs"
                 ValidatorId="Lesson2Step11Validator">
    <Dependencies>
        <Dependency>samples/TaskData_Stage2.cs</Dependency>
    </Dependencies>
</CSharpExercise>]
