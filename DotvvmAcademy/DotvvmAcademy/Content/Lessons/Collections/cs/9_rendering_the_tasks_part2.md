Zobrazení úkolů
===============
Do tagu `<div>` bychom ještě chtěli zobrazit název každého úkolu. Pokud chcete zobrazit text přímo do stránky,
můžete použít data-binding a využít syntaxi se zdvojenými složenými závorkami takto: `{{value: Title}}`.

Nebo můžete využít komponenty `<dot:Literal>` a její vlastnosti `Text`: `<dot:Literal Text="{value: Title}" />`

Tímto způsobem budeme generovat názvy našich úkolů. Taktéž přidejte komponentu `<dot:LinkButton>` do tagu `<div>`.
Tohoho tlačítka využijem k tomu, abychom mohli úkoly označovat jako hotové.

[<DothtmlExercise Initial="samples/ToDoListView_Stage6.dothtml"
                  Final="samples/ToDoListView_Stage7.dothtml"
                  DisplayName="ToDoListView.dothtml"
                  ValidatorId="Lesson2Step9Validator" />]

> Komponenta `<dot:LinkButton>` funguje stejně jako `Button`, ale do stránky se vygeneruje odkaz místo tagu `<button>`
