Implementace prostøedí IValidatableObject
=========================================
Validaci vlastností `SubscriptionFrom` a `SubscriptionTo` budeme provádìt v kódu C#.
Musíme napsat validaèní pravidlo, které by zjišovalo, kdy je `SubscriptionFrom` vìtší ne `SubscriptionTo`.

Zaèneme tím, e `CustomerDetailViewModel` musí implementovat prostøedí `IValidatableObject`. Toto prostøedí obsahuje metodu `Validate`, která musí vracet seznam validaèních chyb.
Jsou spojené s chybami, které jsou vráceny validaèními atributy, take se nemusíte starat o další vlastnosti.

Z této metody mùeme vrátit novou validaèní chybu pomocí `yield return new ValidationResult("error message")`. 
Vrate tuto chybu, pokud bude `SubscriptionFrom` vìtší ne `SubscriptionTo`.

[<CSharpExercise Initial="samples/CustomerDetailViewModel_Stage5.cs"
                 Final="samples/CustomerDetailViewModel_Stage6.cs"
                 DisplayName="CustomerDetailViewModel.cs"
                 ValidatorId="Lesson4Step12Validator">
</CSharpExercise>]