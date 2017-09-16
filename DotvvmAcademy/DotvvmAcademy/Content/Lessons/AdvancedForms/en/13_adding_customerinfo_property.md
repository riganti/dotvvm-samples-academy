Adding CustomerInfo Property
============================
Declare a new property called `NewCustomer` of type `CustomerInfo`.

[<sample Correct="../samples/AddingCustomerInfoPropertyCorrect.cs"
         Incorrect="../samples/AddingCustomerInfoPropertyIncorrect.cs"
         Validator="Lesson3Step13Validator">
    <dependencies>
        <dependency>../samples/CustomerInfo.cs</dependency>
        <dependency>../samples/CountryInfoCorrect.cs</dependency>
    </dependencies>
    <allowedTypes>
        <allowedType><![CDATA[System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.CountryInfo>]]></allowedType>
        <allowedType><![CDATA[DotvvmAcademy.Tutorial.ViewModels.CountryInfo]]></allowedType>
        <allowedType><![CDATA[DotvvmAcademy.Tutorial.ViewModels.CustomerInfo]]></allowedType>
    </allowedTypes>
    <allowedMethods>
         <![CDATA[System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.CountryInfo>.Add]]>
    </allowedMethods>
</sample>]