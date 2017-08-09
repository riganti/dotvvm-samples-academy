Using RadioButtons
==================
Add 2 `RadioButton` controls in the page.

The `Text` property specifies the label text for the radio button. Set the `Text` of the first one to "Admin" and the `Text` of the second one to "User".

The `CheckedItem` specifies which viewmodel property will be used. Set this to `Role` on both radio buttons.

And finally, the `CheckedValue` specifies the value that will be stored in the `Role` property. Set the first one to "A" and the second one to "U".

[<sample Correct="../samples/UsingRadioButtonsCorrect.dothtml"
         Incorrect="../samples/UsingRadioButtonsIncorrect.dothtml"
         Validator="Lesson3Step5Validator" />]
