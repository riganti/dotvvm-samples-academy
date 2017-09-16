Initialize Countries Collection
===============================
Create a default constructor and initialize the `Countries` collection with the following states:

* "USA" with `Id` = 1
* "Canada" with `Id` = 2

[<sample Correct="../samples/InitializeCountriesCorrect.cs"
         Incorrect="../samples/InitializeCountriesIncorrect.cs"
         Validator="Lesson3Step10Validator">
    <dependencies>
        <dependency>../samples/CountryInfoCorrect.cs</dependency>
    </dependencies>
    <allowedTypes>
        <allowedType><![CDATA[System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.CountryInfo>]]></allowedType>
        <allowedType><![CDATA[DotvvmAcademy.Tutorial.ViewModels.CountryInfo]]></allowedType>
    </allowedTypes>
    <allowedMethods>
        <allowedMethod><![CDATA[System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.CountryInfo>.Add]]></allowedMethod>
    </allowedMethods>
</sample>]