Completing the Task Part 2
==========================
Now we need to set the `Click` property of the link button to the `CompleteTask` method.

Please note that all bindings inside `Repeater` are not evaluated on viewmodel, but on the corresponding `TaskData` object.
But the `CompleteTask` method is in the parent scope - we have declared it in the viewmodel.

You can use the `_parent.CompleteTask()` to reference the method from the parent scope.
When supplying arguments to the method, you begin also in the context of `TaskData`, so you can use `_this` to pass the whole
`TaskData` object to the method.

[<DothtmlExercise Initial="samples/ToDoListView_Stage8.dothtml"
                  Final="samples/ToDoListView_Stage9.dothtml"
                  DisplayName="ToDoListView.dothtml"
                  ValidatorId="Lesson2Step12Validator" />]