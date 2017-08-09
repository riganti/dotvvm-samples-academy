Add ViewModel Properties
========================
Declare the `SubscriptionFrom` and `SubscriptionTo` of type `DateTime` in the viewmodel.

We will need to make sure that the first one contains a lower value than the second one.

[<sample Correct="../samples/AddViewModelPropertiesCorrect.cs"
         Incorrect="../samples/AddViewModelPropertiesIncorrect.cs"
         Validator="Lesson4Step11Validator">
    <allowedTypes>
        <allowedType><![CDATA[System.ComponentModel.DataAnnotations.EmailAddressAttribute]]></allowedType>
        <allowedType><![CDATA[System.ComponentModel.DataAnnotations.RequiredAttribute]]></allowedType>
        <allowedType><![CDATA[System.DateTime]]></allowedType>
    </allowedTypes>
    <allowedMethods>
        <allowedMethod><![CDATA[System.ComponentModel.DataAnnotations.RequiredAttribute.IsValid]]></allowedMethod>
        <allowedMethod> <![CDATA[System.ComponentModel.DataAnnotations.EmailAddressAttribute.IsValid]]></allowedMethod>
    </allowedMethods>
</sample>]