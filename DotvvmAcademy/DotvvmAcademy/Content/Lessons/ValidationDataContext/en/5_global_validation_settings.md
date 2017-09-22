Global Validation Settings
==========================
Specifying `Validator.InvalidCssClass` on each validated element is quite annoying. However, you can use this property on any element and it will propagate into all its children.
So you can set this property globally e.g. on the `body` element.

So, you can now remove the `Validator.InvalidCssClass` from the `div`s, wrap the form in the `form` element and use the property on the `form` instead.

[<DothtmlExercise Initial="../samples/CustomerDetailView_Stage5.dothtml"
         Final="../samples/CustomerDetailView_Stage6.dothtml"
         DisplayName="CustomerDetailView.dothtml"
          ValidatorId="Lesson4Step5Validator" />]