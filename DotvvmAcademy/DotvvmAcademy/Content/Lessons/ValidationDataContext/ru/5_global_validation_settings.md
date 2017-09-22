Глобальные настройки Валидаторов
================================
Указание `Validator.InvalidCssClass` на каждом проверяемом элементе очень раздражает. Но, вы можете использовать это свойство на любом элементе, и он будет распространяться на все свои дочерние элементы.
Таким образом, вы можете установить это свойство глобально, например, на `body`.

Итак, теперь вы можете удалить `Validator.InvalidCssClass` из`div`-ов, оберните форму в `form` и используйте свойство на `form`.

[<DothtmlExercise Initial="../samples/CustomerDetailView_Stage5.dothtml"
         Final="../samples/CustomerDetailView_Stage6.dothtml"
         DisplayName="CustomerDetailView.dothtml"
          ValidatorId="Lesson4Step5Validator" />]