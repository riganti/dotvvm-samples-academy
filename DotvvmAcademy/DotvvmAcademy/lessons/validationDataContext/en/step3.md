Validation Basics
=================
In the page, you can invoke the validation using the `Validator.Value` property.
Depending on the settings of the validation, it may for example add a CSS class when some property is not valid.

You can add this property to any HTML element or DotVVM control.
Add the `Validator.Value` property to each `div` and bind it to the property which is used in the `TextBox` inside.

[<sample Correct="../samples/ValidationBasicsCorrect.dothtml"
         Incorrect="../samples/ValidationBasicsIncorrect.dothtml"
         Validator="Lesson4Step3Validator"/>]