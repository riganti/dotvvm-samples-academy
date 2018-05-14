Completing the Task Part 1
==========================
When the user clicks the `LinkButton`, we need to trigger a method in the viewmodel.

The method needs to know, on which task it has been executed. We can supply the task to the method as a parameter.

Declare the `CompleteTask` method which accepts one parameter of type `TaskData`, and sets its `IsCompleted` property to `true`.

[<CSharpExercise Initial="samples/ToDoListViewModel_Stage6.cs"
                 Final="samples/ToDoListViewModel_Stage7.cs"
                 DisplayName="ToDoListViewModel.cs"
                 ValidatorId="Lesson2Step11Validator">
    <Dependencies>
        <Dependency>samples/TaskData_Stage2.cs</Dependency>
    </Dependencies>
</CSharpExercise>]
