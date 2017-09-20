Добавление новой задачи
=======================
Теперь мы должны добавить свойство, которое будет представлять название новой задачи. Давайте назовем его `AddedTaskTitle`.
Не забудьте, что каждый `TextBox` должен иметь соответствующее свойство во viewmodel, иначе, значение введенное пользователем будет потеряно.

Кроме того,нам будет нужен метод `AddTask()` во ViewModel. На данный момент, сделайте его просто пустым. Он не должен возвращать никаких значениий.

[<CSharpExercise Initial="samples/ToDoListViewModel_Stage1.cs"
                 Final="samples/ToDoListViewModel_Stage2.cs"
                 DisplayName="ToDoListViewModel.cs"
                 ValidatorId="Lesson2Step3Validator" />]
