Validation Attributes
=====================
Mark the `City` and `ZIP` properties with the `[Required]` attribute. The validation will make sure that these values are not empty.

Mark the `Email` property with the `[EmailAddress]` attribute. The validation will make sure that this property is either empty, or contains an e-mail address in the correct format.

[<sample Correct="../samples/ValidationAttributesCorrect.cs"
         Incorrect="../samples/ValidationAttributesIncorrect.cs"
         Validator="Lesson4Step2Validator">
    <allowedTypes>
        <allowedType><![CDATA[System.ComponentModel.DataAnnotations.EmailAddressAttribute]]></allowedType>
        <allowedType><![CDATA[System.ComponentModel.DataAnnotations.RequiredAttribute]]></allowedType>
    </allowedTypes>
</sample>]