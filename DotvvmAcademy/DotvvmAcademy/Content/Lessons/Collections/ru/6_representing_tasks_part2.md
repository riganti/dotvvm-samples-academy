Предоставление задач
====================
Теперь давайте вернемся к нашей ViewModel. Нам нужно добавить список объектов `TaskData` в ViewModel,
поэтому мы можем сделать это на странице.

Добавьте свойство `Tasks` во ViewModel. Его тип должен быть `List <TaskData>` и он должен быть инициализирован
в `new List<TaskData>()`.

[<CSharpExercise Initial="samples/ToDoListViewModel_Stage3.cs"
                 Final="samples/ToDoListViewModel_Stage4.cs"
                 DisplayName="ToDoListViewModel.cs"
                 ValidatorId="Lesson2Step6Validator">
    <Dependencies>
        <Dependency>samples/TaskData_Stage2.cs</Dependency>
    </Dependencies>
</CSharpExercise>]