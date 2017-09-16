Adding New Task Part 4
======================
Now, we can implement the `AddTask()` method. It should add a new `TaskData` object with the `Title` property set to `AddedTaskTitle` value.

Also, we'd like to reset the `AddedTaskTitle` property, so after the task is created, assign an empty string in it.

[<CSharpExercise Initial="samples/ToDoListViewModel_Stage5.cs"
                 Final="samples/ToDoListViewModel_Stage6.cs"
                 DisplayName="ToDoListViewModel.cs"
                 ValidatorId="Lesson2Step7Validator">
    <Dependencies>
        <Dependency>samples/TaskData_Stage2.cs</Dependency>
    </Dependencies>
</CSharpExercise>]