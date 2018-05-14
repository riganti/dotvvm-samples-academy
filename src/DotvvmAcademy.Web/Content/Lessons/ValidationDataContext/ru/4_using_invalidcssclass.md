Используем Validation.InvalidCssClass
=====================================
Мы отметили элементы, которые могут взаимодействовать с проверками отдельных свойств.

Теперь мы должны определить, что должно произойти, когда свойство не является действительным.
Мы хотели бы, добавить CSS класс `has-error` ко всем `div`-ам, которые не являются действительными. Это позволит выделить текстовые поля внутри.

Добавьте `Validation.InvalidCssClass="has-error"` каждому `div`.

[<DothtmlExercise Initial="../samples/CustomerDetailView_Stage3.dothtml"
         Final="../samples/CustomerDetailView_Stage4.dothtml"
         DisplayName="CustomerDetailView.dothtml"
          ValidatorId="Lesson4Step4Validator" />]