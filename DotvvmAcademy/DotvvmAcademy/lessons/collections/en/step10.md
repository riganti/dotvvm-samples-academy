Hiding The LinkButton
=====================
The LinkButton should be visible only for tasks which are not completed.

In DotVVM, there is the `Visible` property which you can use on any HTML element or DotVVM control. It can show or hide the element.

Bind the `IsCompleted` property value from the task to the `Visible` property of the `LinkButton`, so the button is hidden when the task is completed.

[<sample Correct="../samples/HideLinkButtonCorrect.dothtml"
         Incorrect="../samples/HideLinkButtonIncorrect.dothtml"
         Validator="Lesson2Step10Validator" />]

> Please note that the `Visible` property can be used also on any HTML element.