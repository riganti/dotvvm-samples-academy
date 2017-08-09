Using ComboBox
==============
Add the `ComboBox` control to the page and bind its `DataSource` to the `Countries` collection. It will fill the combo box with the countries we have initialized in the constructor.

Now we need to tell the combo box that the `Name` property from the `CountryInfo` class should be displayed. Set the `DisplayMember` to "Name".

When the user selects some item, we want to use the `Id` from the selected `CountryInfo` object as selected value. So set the `ValueMember` to "Id".

And finally, we have to specify where the selected value will be stored. Bind the `SelectedValue` to the `SelectedCountryId` property in the viewmodel.

[<sample Correct="../samples/UsingComboBoxCorrect.dothtml"
         Incorrect="../samples/UsingComboBoxIncorrect.dothtml"
         Validator="Lesson3Step11Validator" />]