Реализация IValidatableObject
=============================
Чтобы убедиться в том, что `SubscriptionFrom` меньше, чем `SubscriptionTo`, нам нужно проверить объект с помощью C#.

`Lesson4ViewModel` должен реализовывать интерфейс `IValidatableObject`. Этот интерфейс содержит метод `Validate`, который должен возвращать список ошибок проверки.
Они объединены с ошибками, возвращенных атрибутов проверки, так что вам не нужно заботиться о других свойствах.

Вы можете вернуть ошибку с помощью `yield return new ValidationResult("error message")`. Верните его, если `SubscriptionFrom` больше` SubscriptionTo`.

[<CSharpExercise Initial="samples/CustomerDetailViewModel_Stage5.cs"
                 Final="samples/CustomerDetailViewModel_Stage6.cs"
                 DisplayName="CustomerDetailViewModel.cs"
                 ValidatorId="Lesson4Step12Validator">
</CSharpExercise>]