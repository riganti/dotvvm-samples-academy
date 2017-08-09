Fix the ComboBox Error
======================
Now, we have an error in the code, because the `ComboBox` has the `DataSource` set to `Countries` which doesn't exists in the `NewCustomer` object
because a list of all countries is not a part of the customer information.

This collection is in the page viewmodel, so basically we need to access the parent binding context.

Change the binding of the `DataSource` control to `_parent.Countries` to fix the issue.

> In every binding expression, you can use `_parent`, `_this` and `_root` variables.
> * `_parent` represents the parent binding context
> * `_this` is actual context
> * `_root` is always the viewmodel of the whole page.

[<sample Correct="../samples/FixComboBoxCorrect.dothtml"
         Incorrect="../samples/FixComboBoxIncorrect.dothtml"
         Validator="Lesson3Step15Validator" />]