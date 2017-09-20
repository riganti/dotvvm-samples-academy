Добавление новой Задачи
=======================
Теперь мы можем реализовать метод `AddTask()`. Он должен добавлять новый объект `TaskData` в лист `Tasks`, в котором свойство `Title` принимает значение `AddedTaskTitle`.

Кроме того, мы хотели бы сбросить свойство `AddedTaskTitle`, поэтому, после создания задачи, присвоим ему пустую строку.

[<CSharpExercise Initial="samples/ToDoListViewModel_Stage5.cs"
                 Final="samples/ToDoListViewModel_Stage6.cs"
                 DisplayName="ToDoListViewModel.cs"
                 ValidatorId="Lesson2Step7Validator">
    <Dependencies>
        <Dependency>samples/TaskData_Stage2.cs</Dependency>
    </Dependencies>
</CSharpExercise>]