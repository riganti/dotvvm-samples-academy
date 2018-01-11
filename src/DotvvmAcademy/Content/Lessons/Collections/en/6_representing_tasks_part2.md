Representing Tasks Part 2
=========================
Now let's go back to our viewmodel. We need to add a list of `TaskData` objects in the viewmodel,
so we can render it in the page.

Add the `Tasks` property to the viewmodel. Its type should be `List<TaskData>` and it should be initialized
to `new List<TaskData>()`.

[<CSharpExercise Initial="samples/ToDoListViewModel_Stage3.cs"
                 Final="samples/ToDoListViewModel_Stage4.cs"
                 DisplayName="ToDoListViewModel.cs"
                 ValidatorId="Lesson2Step6Validator">
    <Dependencies>
        <Dependency>samples/TaskData_Stage2.cs</Dependency>
    </Dependencies>
</CSharpExercise>]