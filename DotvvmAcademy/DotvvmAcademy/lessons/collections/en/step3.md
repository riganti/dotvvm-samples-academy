Adding New Task
===============
Now we should add a property which will represent the title of the new task. Let's name it `AddedTaskTitle`.
Don't forgot that every `TextBox` must have its property in the viewmodel, otherwise, the value entered by the user would be lost.

Also, we will need the `AddTask()` method in the viewmodel. For now, make it just empty. It should not return any value.

[<sample Correct="../samples/AddingNewTaskViewModelCorrect.cs"
         Incorrect="../samples/AddingNewTaskViewModelIncorrect.cs"
         Validator="Lesson2Step3Validator" />]
