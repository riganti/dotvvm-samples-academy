Implement IValidatableObject
============================
To make sure that `SubscriptionFrom` is grather than `SubscriptionTo`, we need to validate the object using C# code.

The `Lesson4ViewModel` must implement the `IValidatableObject` interface. This interface contains the `Validate` method, which should return list of the validation errors.
They are combined with the errors returned by the validation attributes, so you don't need to care about the other properties.

You can return the error using the `yield return new ValidationResult("error message")`. Return it if the `SubscriptionFrom` is greater than `SubscriptionTo`.

[<sample Correct="../samples/ImplementIValidatableObjectCorrect.cs"
         Incorrect="../samples/ImplementIValidatableObjectIncorrect.cs"
         Validator="Lesson4Step13Validator">
    <allowedTypes>
        <allowedType><![CDATA[System.ComponentModel.DataAnnotations.EmailAddressAttribute]]></allowedType>
        <allowedType><![CDATA[System.ComponentModel.DataAnnotations.RequiredAttribute]]></allowedType>
        <allowedType><![CDATA[System.DateTime]]></allowedType>
        <allowedType><![CDATA[System.ComponentModel.DataAnnotations.IValidatableObject]]></allowedType>
        <allowedType><![CDATA[System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult>]]></allowedType>
        <allowedType><![CDATA[System.ComponentModel.DataAnnotations.ValidationResult]]></allowedType>
    </allowedTypes>
    <allowedMethods>
        <allowedMethod><![CDATA[System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult>.Add]]></allowedMethod>
    </allowedMethods>
</sample>]