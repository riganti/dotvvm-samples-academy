Using DataContext Property
==========================
Now the bindings in the page are not correct, because the viewmodel doesn't contain the `FirstName` and the other properties.
They changed to `NewCustomer.FirstName`.

In real apps, you want to avoid complex bindings like `NewCustomer.FirstName`. Instead, you can use the `DataContext` property.
You can set the property on any HTML element or DotVVM control.

This property tells DotVVM that all bindings inside this element or control are evaluated on the expression you pass to the `DataContext`.

So, set the DataContext on the `p` start tag like this: `<p DataContext="{value: NewCustomer}">`. You can keep the databindings inside as they are.

[<sample Correct="../samples/UsingDataContextCorrect.dothtml"
         Incorrect="../samples/UsingDataContextIncorrect.dothtml"
         Validator="Lesson3Step14Validator"/>]