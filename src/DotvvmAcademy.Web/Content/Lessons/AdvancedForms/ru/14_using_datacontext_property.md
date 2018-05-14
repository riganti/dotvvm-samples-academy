Используем свойство DataContext
===============================
Теперь привязки на странице не правильные, потому что ViewModel не содержит `FirstName` и другие свойства.
Они изменили на `NewCustomer.FirstName`.

В реальных приложениях, вы хотите, чтобы избежать сложных привязок, как `NewCustomer.FirstName`. Вместо этого, вы можете использовать свойство `DataContext`.
Вы можете установить свойство любого HTML или DotVVM элемента.

Это свойство говорит DotVVM, что все привязки внутри данного элемента или управления оцениваются на выражение, которое вы передаете в `DataContext`.

Таким образом, обернув всю форму в `<div DataContext="{value: NewCustomer}">`. Вы можете держать DataBindings внутри, без изменений.

[<DothtmlExercise Initial="samples/CustomerDetailView_Stage9.dothtml"
        Final="samples/CustomerDetailView_Stage10.dothtml"
        DisplayName="CustomerDetailView.dothtml"
        ValidatorId="Lesson3Step11Validator" />]