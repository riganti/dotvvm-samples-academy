Zobrazení úkolů
===============
Jsme připraveni zobrazit seznam úkolů na stránce. Pro každý úkol budeme chtít vygenerovat tento HTML tag:

```DOTHTML
<div class="task">
...
</div>
```

K tomu využijeme další DotVVM komponenty: `<dot:Repeater>`. Přijdejte jí na stránku a propojte její vlastnost `DataSource` s vlastností `Tasks` ve viewmodelu.
Dovnitř do komponenty umístěte `<div class="task"></div>`. Komponenta vygeneruje tento HTML tag pro každý prvek v seznamu.

[<DothtmlExercise Initial="samples/ToDoListView_Stage4.dothtml"
                  Final="samples/ToDoListView_Stage5.dothtml"
                  DisplayName="ToDoListView.dothtml"
                  ValidatorId="Lesson2Step8Validator" />]