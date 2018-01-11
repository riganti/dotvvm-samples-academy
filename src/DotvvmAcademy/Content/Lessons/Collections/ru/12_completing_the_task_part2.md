Выполнение задачи
=================
Теперь нам нужно установить свойство `Click` кнопки-ссылки на вызов метода `CompleteTask`.

Обратите внимание, что все привязки внутри `Repeater` не описаны в /*текущей*/ ViewModel, но находятся в соответствующем объекте `TaskData`.
Но метод `CompleteTask` в родительской области, так как мы объявили его в /*текущей*/ ViewModel.

Вы можете использовать `_parent.CompleteTask()` для ссылки на метод из родительской области.
При передаче аргументов метода, вы находитесь также в контексте `TaskData`, так что вы можете использовать` _this`, что бы обратиться к 
нужному методу объекта `TaskData`.

[<DothtmlExercise Initial="samples/ToDoListView_Stage8.dothtml"
                  Final="samples/ToDoListView_Stage9.dothtml"
                  DisplayName="ToDoListView.dothtml"
                  ValidatorId="Lesson2Step12Validator" />]