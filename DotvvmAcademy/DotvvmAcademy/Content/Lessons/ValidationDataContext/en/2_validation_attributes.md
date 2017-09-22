Validation Attributes
=====================
Mark the `City` and `ZIP` properties with the `[Required]` attribute. The validation will make sure that these values are not empty.

Mark the `Email` property with the `[EmailAddress]` attribute. The validation will make sure that this property is either empty, or contains an e-mail address in the correct format.

[<CSharpExercise Initial="samples/CustomerDetailViewModel_Stage1.cs"
                 Final="samples/CustomerDetailViewModel_Stage2.cs"
                 DisplayName="CustomerDetailViewModel.cs"
                 ValidatorId="Lesson4Step2Validator">
</CSharpExercise>]