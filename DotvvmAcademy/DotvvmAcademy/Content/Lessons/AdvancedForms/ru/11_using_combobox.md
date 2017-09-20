Используем ComboBox
===================
Добавьте элемент `ComboBox` на страницу и свяжите его свойство `DataSource` с `Countries`. `ComboBox` будет наполнен списком со странами, которые мы инициализируем в конструкторе.

Элементу `ComboBox` нужно указать , что свойство `Name` из класса `CountryInfo` должно быть отображено. Установите `DisplayMember` как "Name".

Когда пользователь выбирает какой-то элемент, мы хотим использовать `Id` от выбранного объекта` CountryInfo` в качестве выбранного значения. Установим `ValueMember` на "Id".

И, наконец, мы должны определить, где выбранное значение будет сохранено. Свяжите `SelectedValue` со свойством `SelectedCountryId` в ViewModel.

[<DothtmlExercise Initial="samples/CustomerDetailView_Stage7.dothtml"
        Final="samples/CustomerDetailView_Stage8.dothtml"
        DisplayName="CustomerDetailView.dothtml"
        ValidatorId="Lesson3Step11Validator" />]