Отображение задач
=================
Внутри `<div>`, мы хотели бы, отобразить название задачи. Если вы хотите вывести текст непосредственно на странице,
вы можете использовать синтаксис привязки данных с двойными фигурными скобками, например: `{{value: Title}}`.

Кроме того, вы можете использовать `<dot:Literal Text="{value: Title}" />`, чтобы написать текст.

Таким образом, отобразим `Title` каждой задачи внутри `<div>`. Кроме того, добавьте `<dot:LinkButton>` внутри `<div>`. Мы будем использовать эту кнопку для обозначения задачи, как выполненную.

[<DothtmlExercise Initial="samples/ToDoListView_Stage6.dothtml"
                  Final="samples/ToDoListView_Stage7.dothtml"
                  DisplayName="ToDoListView.dothtml"
                  ValidatorId="Lesson2Step9Validator" />]

> `LinkButton` работает точно также как и `Button`, но представляет собой ссылку.
