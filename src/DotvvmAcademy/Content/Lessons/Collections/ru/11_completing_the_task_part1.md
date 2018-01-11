Выполнение задачи
=================
Когда пользователь нажмет на `LinkButton`, нам нужно вызвать метод в ViewModel.

Метод должен знать, какая задача была выполнена. Мы можем передать задачу методу в качестве параметра.

Объявите метод `CompleteTask` который принимает один параметр типа `TaskData`, и устанавливает его свойство `IsCompleted` как `true`.

[<CSharpExercise Initial="samples/ToDoListViewModel_Stage6.cs"
                 Final="samples/ToDoListViewModel_Stage7.cs"
                 DisplayName="ToDoListViewModel.cs"
                 ValidatorId="Lesson2Step11Validator">
    <Dependencies>
        <Dependency>samples/TaskData_Stage2.cs</Dependency>
    </Dependencies>
</CSharpExercise>]
