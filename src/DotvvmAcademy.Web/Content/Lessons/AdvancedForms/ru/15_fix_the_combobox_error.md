Исправляем ошибку ComboBox
================================
Теперь у нас есть ошибка в коде, потому что `ComboBox` имеет `DataSource` установленный как `Countries`, который не существует в объекте `NewCustomer`,
потому что список всех стран, не является частью информации о клиенте.

Эта коллекция находится в ViewModel, поэтому в основном мы должны получить доступ к родительской привязки контекста.

Измените привязку `DataSource` на  `_parent.Countries`, чтобы устранить проблему.

> В каждом выражении-связывания (binding) вы можете использовать переменные `_parent`, `_this` и `_root`. 
> * `_parent` represents the parent binding context
> * `_this` актуальный контекст
> * `_root` всегда viewmodel страницы.

[<DothtmlExercise Initial="samples/CustomerDetailView_Stage11.dothtml"
        Final="samples/CustomerDetailView_Stage12.dothtml"
        DisplayName="CustomerDetailView.dothtml"
        ValidatorId="Lesson3Step11Validator" />]