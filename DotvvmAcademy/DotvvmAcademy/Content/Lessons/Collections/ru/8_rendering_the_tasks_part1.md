Отображение Задач
=================
Мы готовы показать список задач. Для каждой задачи, мы хотели бы, использовать этот фрагмент кода HTML:

```DOTHTML
<div class="task">
...
</div>
```

Для этого мы будем использовать `<dot:Repeater>`. Добавьте его на страницу, свяжите его свойство `DataSource` со свойством `Tasks` во ViewModel,
и внутри `<dot:Repeater>`, поместите элемент `<div class="task"></div>`. Он будет повторять `div` для каждого объекта в коллекции.

[<DothtmlExercise Initial="samples/ToDoListView_Stage4.dothtml"
                  Final="samples/ToDoListView_Stage5.dothtml"
                  DisplayName="ToDoListView.dothtml"
                  ValidatorId="Lesson2Step8Validator" />]