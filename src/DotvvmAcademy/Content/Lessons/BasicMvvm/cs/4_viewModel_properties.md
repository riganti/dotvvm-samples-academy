Vlastnosti ViewModelu
=====================
V DotVVM má každá stránka (view) svůj viewmodel. Ve viewmodelu jsou uložené údaje z komponent na stránce nebo cokoli dalšího, co se může dynamicky měnit a interaktovat s uživatelem.

Viewmodel je třída v jazyce C#, data jsou v ní uložená ve veřejných vlastnostech.

Nadeklarujte tři vlastnoti - `Number1`, `Number2` and `Result`, všechny musí být typu `int`.

[<CSharpExercise Initial="samples/CalculatorViewModel_Stage1.cs"
                 Final="samples/CalculatorViewModel_Stage2.cs"
                 DisplayName="CalculatorViewModel.cs"
                 ValidatorId="Lesson1Step4Validator" />]

> Údaje zadané uživatelem se uloží do vlastností Number1, Number2. Součet těchto čísel uložíme do vlastnosti `Result` poté, co uživatel klikne na tlačítko.
