Rendering the Tasks Part 1
==========================
We are ready to render a list of tasks right now. For each task, we'd like to render this HTML snippet:

```DOTHTML
<div class="task">
...
</div>
```

To do this, we'll use the `<dot:Repeater>` control. Add it to the page, bind its `DataSource` property to the `Tasks` property in the viewmodel,
and inside the `<dot:Repeater>`, place the `<div class="task"></div>` element. It will repeat the `div` for each object in the collection.

[<DothtmlExercise Initial="samples/ToDoListView_Stage4.dothtml"
                  Final="samples/ToDoListView_Stage5.dothtml"
                  DisplayName="ToDoListView.dothtml"
                  ValidatorId="Lesson2Step8Validator" />]