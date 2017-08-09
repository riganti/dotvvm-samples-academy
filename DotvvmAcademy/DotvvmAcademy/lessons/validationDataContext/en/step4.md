Using Validation.InvalidCssClass
================================
We have marked elements which can interact with the validation of the individual properties.

Now we have to specify what should happen when the property is not valid.
We'd like to add a CSS class `has-error` to the `div`s which are not valid. This will highlight the textboxes inside.

Add the `Validator.InvalidCssClass="has-error"` to each `div`.

[<sample Correct="../samples/UsingInvalidCssClassCorrect.dothtml"
         Incorrect="../samples/UsingInvalidCssClassIncorrect.dothtml"
         Validator="Lesson4Step4Validator" />]